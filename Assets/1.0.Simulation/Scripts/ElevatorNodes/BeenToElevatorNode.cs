using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeenNotInElevatorNode : Node
{
    AiTreeConstructor ai;

    public BeenNotInElevatorNode(AiTreeConstructor ai)
    {
        this.ai = ai;
    }

    public override NodeState Evaluate()
    {
        if (ai.beenToElavator)
        {
            return NodeState.SUCCESS;
        }
        return NodeState.FAILURE;
    }
}
