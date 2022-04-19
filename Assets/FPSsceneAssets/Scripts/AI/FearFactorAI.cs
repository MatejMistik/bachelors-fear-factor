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
    public static bool needHealing = true;


    [SerializeField] float SetMaxForFear;



    void Start()
    {
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



}
