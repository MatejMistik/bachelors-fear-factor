using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class FearFactorAI : MonoBehaviour
{
    public Transform player;
    public Slider slider;
    AnimatorAI ai;
    public TextMeshProUGUI captions;
    captionsForAI captionsForAI;
    


    private float maxFear;
    public float fear;
    private float fearNormalizedValue;
    public static bool needHealing = true;
    public float decreaseFearTimer;

    private int messagesCounter = 0;
    private string[] stringArrObserving = new string[] { "Can you please stop following me ?", "What exactly is your problem ?", "Get Lost !" };
    private string[] stringArrRunningAway = new string[] { "Let me be ! ", " I am calling the police !", "Get Lost !" };
    private bool wordResponseCalled;

    [SerializeField] float SetMaxForFear;
    public bool canGainFear;

    public bool canLoseFear { get; private set; }

    void Start()
    {
        canGainFear = true;
        canLoseFear = true;
        ai = GetComponent<AnimatorAI>();
        maxFear = SetMaxForFear;
        captionsForAI = GameObject.Find("Captions").GetComponent<captionsForAI>();

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
        Debug.Log(decreaseFearTimer);
        Debug.Log(fear);
        if(fear > 30 && !wordResponseCalled)
        {
            WordResponse();
            wordResponseCalled = true;
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
            fear += 10f;
            Invoke(nameof(ResetCanGainFear), 1f);
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

    public void WordResponse()
    {
        captionsForAI.TurnOnCaptions();
        if(messagesCounter >= stringArrObserving.Length)
        {
            messagesCounter = 0;
        }
        captions.SetText(stringArrObserving[messagesCounter]);
        messagesCounter++;
        Invoke(nameof(ResetWordResponse), 2f);
        
    }

    private void ResetWordResponse()
    {
        wordResponseCalled = false;
    }




}
