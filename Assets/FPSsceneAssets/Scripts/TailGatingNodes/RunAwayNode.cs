using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RunAwayNode : Node
{

    AnimatorAI ai;
    NavMeshAgent agent;

    public RunAwayNode(AnimatorAI ai, NavMeshAgent agent)
    {
        this.ai = ai;
        this.agent = agent;
    }

    public override NodeState Evaluate()
    {

        Debug.Log("RunAwayNode " + this.nodeState);
        agent.speed = 15;
        ai.RunAwayFromPlayer();
        return NodeState.RUNNING;
    }
}
