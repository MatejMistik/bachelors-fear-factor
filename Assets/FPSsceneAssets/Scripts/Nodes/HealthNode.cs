using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthNode : Node
{
    private NewEnemyHealth health;
    private FearFactorAI fear;

    public HealthNode(NewEnemyHealth health, FearFactorAI fear)
    {
        this.health = health;
        this.fear = fear;
    }

    public override NodeState Evaluate()
    {
        if(health.newEnemycurrentHealth <= health.treshold || fear.slider.value >= 0.6)
        {
            return NodeState.SUCCESS;
        }
        return NodeState.FAILURE;
    }

}