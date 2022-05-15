using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class KilledNextToMeNode : Node
{
    AlliesAround alliesAround;

    public KilledNextToMeNode(AlliesAround alliesAround)
    {
        this.alliesAround = alliesAround;
    }

    public override NodeState Evaluate()
    {

        GameObject[] deadEnemies = alliesAround.EnemiesNearbyDead();

        for (int i = 0; i < deadEnemies.Length; i++)
        {
            //Debug.Log(deadEnemies[i]);
            AiKilled aiKilledCorpse = deadEnemies[i].GetComponent<AiKilled>();
            if (aiKilledCorpse.killedTimer >= 0)
            {
                return NodeState.SUCCESS;
            }
        }
        return NodeState.FAILURE;
    }
}
