using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorsClosedNode : Node
{
    ElevatorCheck elevatorCheck;
    public override NodeState Evaluate()
    {
        return elevatorCheck.doorsOpened ? NodeState.FAILURE : NodeState.SUCCESS;
    }
}
