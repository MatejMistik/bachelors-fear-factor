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


    private float maxFear;
    public float fear;
    private float fearNormalizedValue;
    public static bool needHealing = true;
    public float decreaseFearTimer;


    [SerializeField] float SetMaxForFear;
    public bool canGainFear;

    public bool canLoseFear { get; private set; }

    void Start()
    {
        canGainFear = true;
        canLoseFear = true;
        ai = GetComponent<AnimatorAI>();
        maxFear = SetMaxForFear;
        
    }

    // Update is called once per frame
    void Update()
    {
        slider.value = CalculateFear();
        slider.transform.LookAt(player);
        if (fear > 0 && fear < 0.9)
            decreaseFearTimer += Time.deltaTime;
        if (decreaseFearTimer >= 5)
            LoseFearOverTime();

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





}
