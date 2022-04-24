using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class IsElelevatorOpenedNode : Node
{
    private ElevatorCheck elevatorCheck;

    public IsElelevatorOpenedNode(ElevatorCheck elevatorCheck)
    {
        this.elevatorCheck = elevatorCheck;
    }

    public override NodeState Evaluate()
    {
        
        return elevatorCheck.IsDoorsOpened() ? NodeState.SUCCESS : NodeState.FAILURE;

    }

}
