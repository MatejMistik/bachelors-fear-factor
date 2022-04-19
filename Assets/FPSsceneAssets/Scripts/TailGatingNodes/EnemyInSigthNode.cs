using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SensorToolkit;
public class EnemyInSigthNode : Node
{

    Sensor sensor;

    public EnemyInSigthNode(Sensor sensor)
    {
        this.sensor = sensor;
    }

    public override NodeState Evaluate()
    {
        if (sensor.GetNearestByName("FirstPersonPlayer"))
        {
            return NodeState.SUCCESS;
        }

        return NodeState.FAILURE;
    }
}
