using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthNode : Node
{
    private NewEnemyHealth health;

    public HealthNode(NewEnemyHealth health)
    {
        this.health = health;
    }

    public override NodeState Evaluate()
    {
        //Debug.Log(health.newEnemycurrentHealth<= health.treshold ? NodeState.SUCCESS : NodeState.FAILURE);
        return health.newEnemycurrentHealth <= health.treshold ? NodeState.SUCCESS : NodeState.FAILURE;
    }
}