using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiFOV : MonoBehaviour
{
    FearFactorAI fearFactorAi;

    [SerializeField] private float FovOnStart = 80f;
    [SerializeField] private float FovCap = 180f;
    private float _valueFOV;
    private float deltaFOVStartCap;

    public float ValueFOV
    {
        get { return _valueFOV; }
        private set { _valueFOV = value; }
    }

    // Start is called before the first frame update
    void Start()
    {
        deltaFOVStartCap = FovCap - FovOnStart;
        _valueFOV = FovOnStart;
        fearFactorAi = GetComponent<FearFactorAI>();
    }

    // Update is called once per frame
    void Update()
    {
        _valueFOV = FovOnStart + fearFactorAi.slider.value * deltaFOVStartCap;
        Debug.Log(_valueFOV);
    }
}
