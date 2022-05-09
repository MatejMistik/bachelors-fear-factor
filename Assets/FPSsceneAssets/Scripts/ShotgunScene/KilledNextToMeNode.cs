using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SensorToolkit;

public class KilledNextToMeNode : Node
{
    AiHealth aiHealth;
    AlliesAround alliesAround;

    public KilledNextToMeNode(AiHealth aiHealth, AlliesAround alliesAround)
    {
        this.aiHealth = aiHealth;
        this.alliesAround = alliesAround;
    }

    public override NodeState Evaluate()
    {

        GameObject[] deadEnemies = alliesAround.EnemiesNearbyDead();

        for (int i = 0; i < deadEnemies.Length; i++)
        {
            AiKilled aiKilledCorpse = deadEnemies[i].GetComponent<AiKilled>();
            if (aiKilledCorpse.killedTimer >= 0)
            {
                aiHealth.Die();
                return NodeState.SUCCESS;
            }
        }
        return NodeState.FAILURE;
    }
}
