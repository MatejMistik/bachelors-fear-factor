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
        Debug.Log(standingPosition.transform.position);
        Debug.Log(agent.transform.position);
        float distance = Vector3.Distance(standingPosition.transform.position, agent.transform.position);
        if(distance < 0.2)
        {
            Debug.Log(this.nodeState);
            return NodeState.SUCCESS;
        }

        return NodeState.FAILURE;

    }

}
