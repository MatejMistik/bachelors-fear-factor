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

[System.Serializable]
public abstract class Node
{
	protected NodeState _nodeState;
	public NodeState nodeState { get { return _nodeState; } }

	public abstract NodeState Evaluate();
}

public enum NodeState
{
	RUNNING, SUCCESS, FAILURE,
}