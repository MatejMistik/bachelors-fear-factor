using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SensorToolkit;
using UnityEngine.AI;

public class PatrollingNode : Node
{

    NavigationPathForAI NavigationPath;
    NavMeshAgent agent;
    AiTreeConstructor ai;
    FearFactorAI fearFactorAI;

    public PatrollingNode(NavigationPathForAI navigationPath, NavMeshAgent agent, AiTreeConstructor ai, FearFactorAI fearFactorAI)
    {
        NavigationPath = navigationPath;
        this.agent = agent;
        this.ai = ai;
        this.fearFactorAI = fearFactorAI;
    }

    public override NodeState Evaluate()
    {
        fearFactorAI.WhichStateIsIn(FearFactorAI.FearState.Calm);
        //Debug.Log("Patrolling " + this.nodeState);
        ai.nodeStateText.SetText("Patrolling");
        agent.speed = 8;
        NavigationPath.StartPatrolling();
        return NodeState.RUNNING;
    }
}
