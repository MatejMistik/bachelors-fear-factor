using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SensorToolkit;

public class PlayerNearbyNode : Node
{
    Sensor sensor;
    private bool agentRegistered = false;
    public PlayerNearbyNode(Sensor sensor)
    {
        this.sensor = sensor;
    }

    public override NodeState Evaluate()
    {
        if (sensor.GetNearestByName("FirstPersonPlayer") || agentRegistered)
        {
            Debug.Log("registered");
            agentRegistered = true;
            return NodeState.SUCCESS;
        }
        
        return NodeState.FAILURE;
    }
}
