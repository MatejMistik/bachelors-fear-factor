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
        Debug.Log(health.healthRestored);
        //Debug.Log(agent.velocity.magnitude); 
        if((health.newEnemycurrentHealth  < health.maxHealth) && agent.velocity.magnitude == 0 && !health.healthRestored )
        {
            health.Restore(10f);
            
            return NodeState.SUCCESS;
        }
        Debug.Log(fearFactorAI.needToLooseAllFear);
        if (fearFactorAI.needToLooseAllFear && agent.velocity.magnitude == 0)
        {
            fearFactorAI.fear = 0f;
            return NodeState.SUCCESS;
        }
        Debug.Log(nodeState);
        fearFactorAI.needToLooseAllFear = false;
        return NodeState.FAILURE;
    }
}
