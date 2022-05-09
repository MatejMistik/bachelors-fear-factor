using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CheckOnCorpseNode : Node
{
    NavMeshAgent agent;
    AlliesAround alliesAround;

    public override NodeState Evaluate()
    {
        float distance = Vector3.Distance(alliesAround.enemiesToBeChecked[0].transform.position, agent.transform.position);
        if(distance < 0.2f)
        {
            AiCorpseCheck aiCorpse = alliesAround.enemiesToBeChecked[0].GetComponent<AiCorpseCheck>();
            aiCorpse.corpseWasChecked = true;
            return NodeState.SUCCESS;
        }
        agent.SetDestination(alliesAround.enemiesToBeChecked[0].transform.position);
        return NodeState.RUNNING;

        
    }
}
