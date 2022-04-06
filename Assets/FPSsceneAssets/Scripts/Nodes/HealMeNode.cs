using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class HealMeNode : Node
{
    private NewEnemyHealth health;
    public NavMeshAgent agent;

    public HealMeNode(NewEnemyHealth health, NavMeshAgent agent)
    {
        this.health = health;
        this.agent = agent;
    }

    public override NodeState Evaluate()
    {
        Debug.Log(agent.velocity.magnitude); 
        if(health.newEnemycurrentHealth < health.maxHealth && agent.velocity.magnitude == 0)
        {
            health.Restore();
            return NodeState.SUCCESS;
        }
        return NodeState.FAILURE;
    }
}
