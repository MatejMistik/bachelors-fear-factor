using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunAwayNode : Node
{

    AnimatorAI ai;

    public RunAwayNode(AnimatorAI ai)
    {
        this.ai = ai;
    }

    public override NodeState Evaluate()
    {
        Debug.Log("RunAwayNode " + this.nodeState);
        ai.RunAwayFromPlayer();
        return NodeState.RUNNING;
    }
}
