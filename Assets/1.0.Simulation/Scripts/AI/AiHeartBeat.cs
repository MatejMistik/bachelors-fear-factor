using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiHeartBeat : MonoBehaviour
{
    FearFactorAI fearFactorAi;


    [SerializeField] private float startingHeartBeat;
    [SerializeField] private float heartBeatCap;
    private float _heartBeat;

    public float heartBeat
    {
        get { return _heartBeat; }
        private set { _heartBeat = value; }
    }

    // Start is called before the first frame update
    void Start()
    {
        _heartBeat = startingHeartBeat;
        fearFactorAi = GetComponent<FearFactorAI>();
    }

    // Update is called once per frame
    void Update()
    {
        _heartBeat = startingHeartBeat + fearFactorAi.fear * heartBeatCap;
    }
}
