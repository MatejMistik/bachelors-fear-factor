using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ShootingNode : Node
{
    public NavMeshAgent agent;
    private AnimatorAI ai;
    private Transform playerTransform;

    public ShootingNode(NavMeshAgent agent, AnimatorAI ai, Transform playerTransform)
    {
        this.agent = agent;
        this.ai = ai;
        this.playerTransform = playerTransform;
    }

    public override NodeState Evaluate()
    {
        agent.isStopped = true;
        ai.DebugMessage("Shooting");
        return NodeState.RUNNING;
    }

}