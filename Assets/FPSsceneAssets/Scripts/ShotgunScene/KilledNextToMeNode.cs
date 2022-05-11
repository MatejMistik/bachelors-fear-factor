using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class KilledNextToMeNode : Node
{
    AlliesAround alliesAround;
    FearFactorAI fearFactorAI;
    NavMeshAgent agent;
    AiTreeConstructor ai;

    public KilledNextToMeNode(AlliesAround alliesAround, FearFactorAI fearFactorAI, NavMeshAgent agent, AiTreeConstructor ai)
    {
        this.alliesAround = alliesAround;
        this.fearFactorAI = fearFactorAI;
        this.agent = agent;
        this.ai = ai;
    }

    public override NodeState Evaluate()
    {

        GameObject[] deadEnemies = alliesAround.EnemiesNearbyDead();

        for (int i = 0; i < deadEnemies.Length; i++)
        {
            Debug.Log(deadEnemies[i]);
            AiKilled aiKilledCorpse = deadEnemies[i].GetComponent<AiKilled>();
            if (aiKilledCorpse.killedTimer >= 0)
            {
                ai.nodeStateText.SetText("Unconsiuos");
                agent.transform.LookAt(aiKilledCorpse.transform);
                fearFactorAI.WhichStateIsIn(FearFactorAI.FearState.SeenDeadBody);
                fearFactorAI.GainFearOverTime();
                return NodeState.SUCCESS;
            }
        }
        return NodeState.FAILURE;
    }
}
