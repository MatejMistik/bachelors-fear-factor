using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using SensorToolkit;
public class EnemyInSigthNode : Node
{

    Sensor sensor;
    NavMeshAgent agent;
    FearFactorAI fear;

    public EnemyInSigthNode(Sensor sensor, NavMeshAgent agent, FearFactorAI fear)
    {
        this.sensor = sensor;
        this.agent = agent;
        this.fear = fear;
    }

    public override NodeState Evaluate()
    {
        if (sensor.GetNearestByName("FirstPersonPlayer"))
        {
            return NodeState.SUCCESS;
        }
        agent.isStopped = false;
        return NodeState.FAILURE;
    }
}
