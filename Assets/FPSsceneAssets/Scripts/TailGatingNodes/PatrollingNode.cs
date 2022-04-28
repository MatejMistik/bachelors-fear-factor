using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SensorToolkit;
using UnityEngine.AI;

public class PatrollingNode : Node
{

    NavigationPathForAI NavigationPath;
    NavMeshAgent agent;
    AnimatorAI ai;

    public PatrollingNode(NavigationPathForAI navigationPath, NavMeshAgent agent, AnimatorAI ai)
    {
        NavigationPath = navigationPath;
        this.agent = agent;
        this.ai = ai;
    }

    public override NodeState Evaluate()
    {
        
        Debug.Log("Patrolling " + this.nodeState);
        ai.nodeStateText.SetText("Patrolling");
        agent.speed = 8;
        NavigationPath.StartPatrolling();
        return NodeState.RUNNING;
    }
}
