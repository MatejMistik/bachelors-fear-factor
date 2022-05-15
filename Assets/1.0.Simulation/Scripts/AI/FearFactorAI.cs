using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class FearFactorAI : MonoBehaviour
{
    public AudioSource audioSource;

    public Transform player;
    public Slider slider;
    AiTreeConstructor ai;
    public TextMeshProUGUI captions;
    CaptionsForAI CaptionsForAI;
    AiHealth aiHealth;
    protected FearState _fearState;
    public FearState fearState { get { return _fearState; } }
    private int actualState = 0;
    public const int CALM = 0;
    public const int FEARED = 1;
    public const int OBSERVING = 2;
    public const int RUNNING = 3;
    public const int SEENDEADBODY = 4;
    public const int UNCONSCIOUS = 5;

    public enum FearState
    {
        Calm,
        Feared,
        Observing,
        Running,
        SeenDeadBody,
        Unconscious,
    }

    private float maxFear;
    public float fear;
    private float fearNormalizedValue;


    public static bool needHealing = true;
    [HideInInspector] public float decreaseFearTimer;
    public float timeBetweenGainOfFear;

    private int messagesCounter = 0;
    private string[] stringArrObserving = new string[] { "Can you please stop following me ?", "What exactly is your problem ?", "Get Lost !" };
    private string[] stringArrRunningAway = new string[] { "Let me be ! ", " I am calling the police !", "Psycho !" };
    private string[] stringArrSeenDeadBody = new string[] { "I am gonna faint" };
    private bool wordResponseCalled = false;
    public bool observingDialogActive;
    private float fearStateMultiplier = 0f;

    [SerializeField] float SetMaxForFear;
    public bool canGainFear;
    private bool audioSourceOn = true;
    private float audioTimer;
    private float maleMultiplier = 1f;

    public bool canLoseFear { get; private set; }

    void Start()
    {
        if (AiConstraintsConfig.female) maleMultiplier = 1.2f;
        canGainFear = true;
        canLoseFear = true;
        ai = GetComponent<AiTreeConstructor>();
        maxFear = SetMaxForFear;
        CaptionsForAI = GameObject.Find("Captions").GetComponent<CaptionsForAI>();
        aiHealth = GetComponent<AiHealth>();
        if (audioSource) {
            audioSource = GetComponent<AudioSource>();
            audioTimer = audioSource.clip.length;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        slider.value = CalculateFear();
        slider.transform.LookAt(player);
        if (fear > 0 && fear < 90)
            decreaseFearTimer += Time.deltaTime;
        if (decreaseFearTimer >= 5)
            LoseFearOverTime();
        //Debug.Log(decreaseFearTimer);
        //Debug.Log(fear);
        // activating dialog
        //Debug.Log(wordResponseCalled);
        //Debug.Log(fear);
        //Debug.Log(actualState);
        if(fear > 30 && !wordResponseCalled && actualState == OBSERVING)
        {
            WordResponse(stringArrObserving);
            wordResponseCalled = true;
        }
        if(fear > 30 && !wordResponseCalled && actualState == RUNNING)
        {
            WordResponse(stringArrRunningAway);
            wordResponseCalled = true;
        }
        //Debug.Log(actualState);
       // Debug.Log(fear);
       // Debug.Log(actualState == SEENDEADBODY && fear > 99);
        if(actualState == SEENDEADBODY && !wordResponseCalled)
        {
            WordResponse(stringArrSeenDeadBody );
            wordResponseCalled = true;
        }
        if(actualState == SEENDEADBODY && fear > 99)
        {
            if (audioSource && audioSourceOn)
            {
                audioSourceOn = false;
                audioSource.Play();
                
            }
            
            if(audioTimer <= 0)
            {
                this.enabled = false;
                aiHealth.Die();
            }
            
            
        }
        if(!audioSourceOn)
        audioTimer -= Time.deltaTime;
        

       


    }
    float CalculateFear()
    {
        return fear / maxFear;
    }

    public void GainFearOverTime()
    {
        if (canGainFear)
        {
            fear += 10f * fearStateMultiplier * maleMultiplier;
            Invoke(nameof(ResetCanGainFear), timeBetweenGainOfFear);
        }
        decreaseFearTimer = 0f;
        canGainFear = false;
        
    }

    private void ResetCanGainFear()
    {
        canGainFear = true;
    }

    public void LoseFearOverTime()
    {
        if (canGainFear)
        {
            fear -= 10f;
            Invoke(nameof(ResetCanLoseFear), 1f);
        }
        decreaseFearTimer = 0f;
        canLoseFear = false;

    }

    private void ResetCanLoseFear()
    {
        canLoseFear = true;
    }

    public void WordResponse(string[] array)
    {
        Debug.Log("captions called");
        CaptionsForAI.TurnOnCaptions();
        if(messagesCounter >= array.Length)
        {
            messagesCounter = 0;
        }
        captions.SetText(array[messagesCounter]);
        messagesCounter++;
        Invoke(nameof(ResetWordResponse), 2f);
        
    }

    private void ResetWordResponse()
    {
        wordResponseCalled = false;
    }

    public void WhichStateIsIn(FearState state)
    {
        switch (state)
        {
            case FearState.Observing:
                actualState = OBSERVING;
                fearStateMultiplier = 1f;
                    break;
            case FearState.Calm:
                actualState = CALM;
                break;
            case FearState.Running:
                actualState = RUNNING;
                fearStateMultiplier = 2f;
                break;
            case FearState.SeenDeadBody:
                actualState = SEENDEADBODY;
                fearStateMultiplier = 2.5f;
                break;

        }
    }

    public void LookedAtDeathBody()
    {
        actualState = SEENDEADBODY;
    }



}
