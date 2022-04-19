using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SensorToolkit;

public class PatrollingNode : Node
{
    AnimatorAI ai;

    public PatrollingNode(AnimatorAI ai)
    {
        this.ai = ai;
    }

    public override NodeState Evaluate()
    {
        Debug.Log("Patrolling " + this.nodeState);
        ai.Patrolling();
        return NodeState.RUNNING;
    }
}
