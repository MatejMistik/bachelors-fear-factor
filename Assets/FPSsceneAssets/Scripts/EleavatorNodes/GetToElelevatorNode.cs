using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GetToElelevatorNode : Node
{
    private NavMeshAgent agent;
    private ElevatorCheck elevatorCheck;

    public GetToElelevatorNode(NavMeshAgent agent, ElevatorCheck elevatorCheck)
    {
        this.agent = agent;
        this.elevatorCheck = elevatorCheck;
    }

    public override NodeState Evaluate()
    {
        Transform standingPosition = elevatorCheck.elevatorStandingPosition;
        float distance = Vector3.Distance(agent.transform.position, standingPosition.transform.position);
        if(distance < 0.2f)
        {
            agent.SetDestination(standingPosition.transform.position);
            return NodeState.RUNNING;
        }
        return NodeState.SUCCESS;
        
    }

}
