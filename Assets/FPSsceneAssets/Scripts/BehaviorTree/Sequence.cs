using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
***************************************************************************************
*	Title: AI in Unity Turorial. Behavior Trees.
*	Author: GameDevChef
*   Date: 22.05., 2022
*	Code version: 1.0
*	Availability: https://github.com/GameDevChef/BehaviourTrees
*
***************************************************************************************/
public class Sequence : Node
{
    protected List<Node> nodes = new List<Node>();

    public Sequence(List<Node> nodes)
    {
        this.nodes = nodes;
    }
    public override NodeState Evaluate()
    {
        bool isAnyNodeRunning = false;
        foreach (var node in nodes)
        {
            switch (node.Evaluate())
            {
                case NodeState.RUNNING:
                    isAnyNodeRunning = true;
                    break;
                case NodeState.SUCCESS:
                    break;
                case NodeState.FAILURE:
                    _nodeState = NodeState.FAILURE;
                    return _nodeState;
                default:
                    break;
            }
        }
        _nodeState = isAnyNodeRunning ? NodeState.RUNNING : NodeState.SUCCESS;
        return _nodeState;
    }
}