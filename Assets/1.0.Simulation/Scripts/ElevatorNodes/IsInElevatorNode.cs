using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class IsInElevatorNode : Node
{
    private NavMeshAgent agent;
    private ElevatorCheck elevatorCheck;
    private AiTreeConstructor ai;

    public IsInElevatorNode(NavMeshAgent agent, ElevatorCheck elevatorCheck, AiTreeConstructor ai)
    {
        this.agent = agent;
        this.elevatorCheck = elevatorCheck;
        this.ai = ai;
    }

    public override NodeState Evaluate()
    {
        Debug.Log(elevatorCheck);
        Transform standingPosition = elevatorCheck.elevatorStandingPosition;
        //Debug.Log(standingPosition.transform.position);
        //Debug.Log(agent.transform.position);
        float distance = Vector3.Distance(standingPosition.transform.position, agent.transform.position);
        //Debug.Log(distance);
        if(distance < 4 && !ai.runningAway)
        {
            Debug.Log("IsInElevator" + this.nodeState);
            return NodeState.SUCCESS;
        }
        Debug.Log("IsInElevatorOutsidetheloop" + this.nodeState);
        return NodeState.FAILURE;

    }

}
