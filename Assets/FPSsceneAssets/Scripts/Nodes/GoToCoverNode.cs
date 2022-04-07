using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GoToCoverNode : Node
{
    private NavMeshAgent agent;
    private AnimatorAI ai;

    public GoToCoverNode(NavMeshAgent agent, AnimatorAI ai)
    {
        this.agent = agent;
        this.ai = ai;
    }

    public override NodeState Evaluate()
    {
        Transform coverSpot = ai.GetBestCoverSpot();
        if (coverSpot == null)
            return NodeState.FAILURE;
        ai.DebugMessage("GoToCoverNode");
        float distance = Vector3.Distance(coverSpot.position, agent.transform.position);
        if (distance > 0.5f)
        {
            ai.nodeStateText.SetText("GoToCover");
            agent.isStopped = false;
            agent.SetDestination(coverSpot.position);
            return NodeState.RUNNING;
        }
        else
        {
            ai.nodeStateText.SetText("InCover");
            agent.isStopped = true;
            return NodeState.SUCCESS;
            
        }
    }


}
