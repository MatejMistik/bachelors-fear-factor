using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Script must be placed on same level Gameobject 

public class HearLossAI : MonoBehaviour
{
    FearFactorAI fearFactorAi;

    [SerializeField] private float HearingRangeOnStart = 80f;
    [SerializeField] private float HearingRangeCap = 180f;
    [SerializeField] private float HearingRangeDuringFear = 180f;
    private float _valueHearing;
    private float deltaHearingStartCap;

    public float ValueHearing
    {
        get { return _valueHearing; }
        private set { _valueHearing = value; }
    }

    // Start is called before the first frame update
    void Start()
    {
        deltaHearingStartCap = HearingRangeCap - HearingRangeOnStart;
        _valueHearing = HearingRangeOnStart;
        fearFactorAi = GetComponent<FearFactorAI>();
    }

    // Update is called once per frame
    void Update()
    {
        _valueHearing = HearingRangeOnStart + fearFactorAi.slider.value * deltaHearingStartCap;
        Debug.Log(_valueHearing);
    }
}