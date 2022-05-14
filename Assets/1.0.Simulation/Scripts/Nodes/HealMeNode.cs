using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class HealMeNode : Node
{
    private AiHealth health;
    public NavMeshAgent agent;
    private FearFactorAI fearFactorAI;

    public HealMeNode(AiHealth health, NavMeshAgent agent, FearFactorAI fearFactorAI)
    {
        this.health = health;
        this.agent = agent;
        this.fearFactorAI = fearFactorAI;
    }

    public override NodeState Evaluate()
    {
        //Debug.Log(agent.velocity.magnitude); 
        if((health.newEnemycurrentHealth  < health.maxHealth || fearFactorAI.slider.value >= 0.6f) && agent.velocity.magnitude == 0 && !health.healthRestored)
        {
            health.Restore(50);
            fearFactorAI.fear = 0;
            return NodeState.SUCCESS;
        }
        return NodeState.FAILURE;
    }
}
