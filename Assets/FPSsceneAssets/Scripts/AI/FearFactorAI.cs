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
    private float fear;
    private float fearNormalizedValue;
    public static bool needHealing = true;


    [SerializeField] float SetMaxForFear;
    public bool canGainFear;

    void Start()
    {
        canGainFear = true;
        ai = GetComponent<AnimatorAI>();
        maxFear = SetMaxForFear;
        
    }

    // Update is called once per frame
    void Update()
    {
        slider.value = CalculateFear();
        slider.transform.LookAt(player);

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
        canGainFear = false;
        
    }

    private void ResetCanGainFear()
    {
        canGainFear = true;
    }



}
