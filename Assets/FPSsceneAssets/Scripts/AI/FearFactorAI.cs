using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class FearFactorAI : MonoBehaviour
{
    public Transform player;
    public Slider slider;
    AiTreeConstructor ai;
    public TextMeshProUGUI captions;
    captionsForAI captionsForAI;
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
    public float decreaseFearTimer;
    public float timeBetweenGainOfFear;

    private int messagesCounter = 0;
    private string[] stringArrObserving = new string[] { "Can you please stop following me ?", "What exactly is your problem ?", "Get Lost !" };
    private string[] stringArrRunningAway = new string[] { "Let me be ! ", " I am calling the police !", "Psycho !" };
    private string[] stringArrSeenDeadBody = new string[] { "I am gonna faint" };
    private bool wordResponseCalled;
    public bool observingDialogActive;
    private float fearStateMultiplier = 0f;

    [SerializeField] float SetMaxForFear;
    public bool canGainFear;

    public bool canLoseFear { get; private set; }

    void Start()
    {
        canGainFear = true;
        canLoseFear = true;
        ai = GetComponent<AiTreeConstructor>();
        maxFear = SetMaxForFear;
        captionsForAI = GameObject.Find("Captions").GetComponent<captionsForAI>();
        aiHealth = GetComponent<AiHealth>();
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
        if(fear > 30 && !wordResponseCalled && actualState == OBSERVING)
        {
            WordResponse(stringArrObserving);
            wordResponseCalled = true;
            return;
        }
        if(fear > 30 && !wordResponseCalled && actualState == RUNNING)
        {
            WordResponse(stringArrRunningAway);
            wordResponseCalled = true;
            return;
        }
        Debug.Log(actualState);
        Debug.Log(fear);
        Debug.Log(actualState == SEENDEADBODY && fear > 99);
        if(actualState == SEENDEADBODY && !wordResponseCalled)
        {
            WordResponse(stringArrSeenDeadBody );
            wordResponseCalled = true;
        }
        if(actualState == SEENDEADBODY && fear > 99)
        {
            this.enabled = false;
            aiHealth.Die();
        }

       


    }
    float CalculateFear()
    {
        return fear / maxFear;
    }

    public void GainFearOverTime()
    {
        if (canGainFear)
        {
            fear += 10f * fearStateMultiplier;
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
        
        captionsForAI.TurnOnCaptions();
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
