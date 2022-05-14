using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenTheDoorNode : Node

{
    private ElevatorCheck elevatorCheck;

    public OpenTheDoorNode(ElevatorCheck elevatorCheck)
    {
        this.elevatorCheck = elevatorCheck;
    }

    public override NodeState Evaluate()
    {
        Debug.Log(elevatorCheck.doorsOpened);
        Debug.Log(elevatorCheck.doorsAreMoving);
        if (!elevatorCheck.doorsOpened && !elevatorCheck.doorsAreMoving)
        {
            Debug.Log("CloseTheDoorSucc" + this.nodeState);
            elevatorCheck.doorsAreMoving = true;
            return NodeState.SUCCESS;
        }
        Debug.Log("CloseTheDoor" + this.nodeState);
        return NodeState.FAILURE;


    }
}
