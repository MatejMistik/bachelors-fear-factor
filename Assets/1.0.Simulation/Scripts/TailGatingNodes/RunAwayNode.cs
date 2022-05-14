using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using SensorToolkit;

public class RunAwayNode : Node
{

    AiTreeConstructor ai;
    NavMeshAgent agent;
    Sensor sensor;
    FearFactorAI fearFactorAI;
    Transform playerTransform;

    public RunAwayNode(AiTreeConstructor ai, NavMeshAgent agent, Sensor sensor, FearFactorAI fearFactorAI, Transform playerTransform)
    {
        this.ai = ai;
        this.agent = agent;
        this.sensor = sensor;
        this.fearFactorAI = fearFactorAI;
        this.playerTransform = playerTransform;
    }

    public override NodeState Evaluate()
    {
        fearFactorAI.WhichStateIsIn(FearFactorAI.FearState.Running);
        Debug.Log("RunAwayNode " + this.nodeState);
        agent.speed = 15;
        // RunAway Function removed from AiTreeConstructor
        Vector3 dirToPlayer = ai.transform.position - playerTransform.position;
        Vector3 newPos = ai.transform.position + dirToPlayer;
        agent.SetDestination(newPos);
        if (sensor.GetNearestByName("FirstPersonPlayer"))
        {
            if (fearFactorAI.canGainFear)
                fearFactorAI.GainFearOverTime();
        }
        ai.nodeStateText.SetText("Running Away");
        return NodeState.RUNNING;
    }
}
