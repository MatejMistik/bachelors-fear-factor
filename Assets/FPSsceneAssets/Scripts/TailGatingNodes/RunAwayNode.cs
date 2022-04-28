using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using SensorToolkit;

public class RunAwayNode : Node
{

    AnimatorAI ai;
    NavMeshAgent agent;
    Sensor sensor;
    FearFactorAI fearFactorAI;

    public RunAwayNode(AnimatorAI ai, NavMeshAgent agent, Sensor sensor, FearFactorAI fearFactorAI)
    {
        this.ai = ai;
        this.agent = agent;
        this.sensor = sensor;
        this.fearFactorAI = fearFactorAI;
    }

    public override NodeState Evaluate()
    {

        Debug.Log("RunAwayNode " + this.nodeState);
        agent.speed = 15;
        ai.RunAwayFromPlayer();
        if (sensor.GetNearestByName("FirstPersonPlayer"))
        {
            if (fearFactorAI.canGainFear)
                fearFactorAI.GainFearOverTime();
        }
        ai.nodeStateText.SetText("Running Away");
        return NodeState.RUNNING;
    }
}
