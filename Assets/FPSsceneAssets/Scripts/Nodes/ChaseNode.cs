using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ChaseNode : Node
{
    public Transform target;
    private NavMeshAgent agent;

    public ChaseNode(Transform target, NavMeshAgent agent)
    {
        this.target = target;
        this.agent = agent;
    }

    public override NodeState Evaluate()
    {
        float distance = Vector3.Distance(target.position, agent.transform.position);
        if(distance > 0.5f)
        {
            agent.isStopped = false;
            agent.SetDestination(target.position);
            return NodeState.RUNNING;
        }
        else
        {
            agent.isStopped = true;
            return NodeState.SUCCESS;
        }
    }

   
}
