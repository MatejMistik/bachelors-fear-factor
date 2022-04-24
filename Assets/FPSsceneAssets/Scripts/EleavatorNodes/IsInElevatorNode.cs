using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class IsInElevatorNode : Node
{
    private NavMeshAgent agent;
    private ElevatorCheck elevatorCheck;

    public IsInElevatorNode(NavMeshAgent agent, ElevatorCheck elevatorCheck)
    {
        this.agent = agent;
        this.elevatorCheck = elevatorCheck;
    }

    public override NodeState Evaluate()
    {
        Transform standingPosition = elevatorCheck.elevatorStandingPosition;
        if(agent.transform.position == standingPosition.transform.position)
        {
            return NodeState.SUCCESS;
        }

        return NodeState.FAILURE;

    }

}
