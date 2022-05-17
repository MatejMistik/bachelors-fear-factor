using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ReactToKilledCorpse : Node
{
    FearFactorAI fearFactorAI;
    NavMeshAgent agent;
    AiTreeConstructor ai;
    AlliesAround alliesAround;

    public ReactToKilledCorpse(FearFactorAI fearFactorAI, NavMeshAgent agent, AiTreeConstructor ai, AlliesAround alliesAround)
    {
        this.fearFactorAI = fearFactorAI;
        this.agent = agent;
        this.ai = ai;
        this.alliesAround = alliesAround;
    }

    public override NodeState Evaluate()
    {

        GameObject[] deadEnemies = alliesAround.EnemiesNearbyDead();

        for (int i = 0; i < deadEnemies.Length; i++)
        {
            Debug.Log(nodeState);
            AiKilled aiKilledCorpse = deadEnemies[i].GetComponent<AiKilled>();
            ai.nodeStateText.SetText("Unconsiuos");
            agent.transform.LookAt(aiKilledCorpse.transform);
            fearFactorAI.WhichStateIsIn(FearFactorAI.FearState.SeenDeadBody);
            fearFactorAI.GainFearOverTime();
            
        }
        return NodeState.RUNNING;


    }

    
}
