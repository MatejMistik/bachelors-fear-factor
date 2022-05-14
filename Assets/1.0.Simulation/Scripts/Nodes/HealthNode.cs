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

public class HealthNode : Node
{
    private AiHealth health;
    private FearFactorAI fear;

    public HealthNode(AiHealth health, FearFactorAI fear)
    {
        this.health = health;
        this.fear = fear;
    }

    public override NodeState Evaluate()
    {
        if(health.newEnemycurrentHealth <= health.treshold || fear.slider.value >= 0.9)
        {
            return NodeState.SUCCESS;
        }
        return NodeState.FAILURE;
    }

}