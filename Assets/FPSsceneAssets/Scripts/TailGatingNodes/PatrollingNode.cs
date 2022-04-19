using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SensorToolkit;
using UnityEngine.AI;

public class PatrollingNode : Node
{

    NavigationPathForAI NavigationPath;
    NavMeshAgent agent;

    public PatrollingNode(NavigationPathForAI navigationPath, NavMeshAgent agent)
    {
        NavigationPath = navigationPath;
        this.agent = agent;
    }

    public override NodeState Evaluate()
    {
        
        Debug.Log("Patrolling " + this.nodeState);
        agent.speed = 8;
        NavigationPath.StartPatrolling();
        return NodeState.RUNNING;
    }
}
