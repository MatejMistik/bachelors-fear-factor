using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WasCorpseChecked : Node
{
    AlliesAround alliesAround;

    public WasCorpseChecked(AlliesAround alliesAround)
    {
        this.alliesAround = alliesAround;
    }

    public override NodeState Evaluate()
    {
        GameObject[] deadEnemies = alliesAround.EnemiesNearbyDead();
        
        for(int i = 0; i < deadEnemies.Length; i++)
        {
            AiCorpseCheck aiCorpse = deadEnemies[i].GetComponent<AiCorpseCheck>();
            if (!aiCorpse.corpseWasChecked)
            {
                alliesAround.enemiesToBeChecked.Add(deadEnemies[i]);
                return NodeState.SUCCESS;
            }
        }

        return NodeState.FAILURE;
    }
}
