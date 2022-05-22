using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiHeartBeat : MonoBehaviour
{
    FearFactorAI fearFactorAi;


    [SerializeField] private float heartBeatOnStart = 80f;
    [SerializeField] private float heartBeatCap = 180f;
    private float _heartBeat;
    private float deltaHeartBeatStartCap;

    public float heartBeat
    {
        get { return _heartBeat; }
        private set { _heartBeat = value; }
    }

    // Start is called before the first frame update
    void Start()
    {
        deltaHeartBeatStartCap = heartBeatCap - heartBeatOnStart;
        _heartBeat = heartBeatOnStart;
        fearFactorAi = GetComponent<FearFactorAI>();
    }

    // Update is called once per frame
    void Update()
    {
        _heartBeat = heartBeatOnStart + fearFactorAi.slider.value * deltaHeartBeatStartCap;
        Debug.Log(_heartBeat);
    }
}
