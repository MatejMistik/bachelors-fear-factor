using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CheckOnCorpseNode : Node
{
    NavMeshAgent agent;
    AlliesAround alliesAround;

    public CheckOnCorpseNode(NavMeshAgent agent, AlliesAround alliesAround)
    {
        this.agent = agent;
        this.alliesAround = alliesAround;
    }

    public override NodeState Evaluate()
    {
        float distance = Vector3.Distance(alliesAround.enemiesToBeChecked[0].transform.position, agent.transform.position);
        Debug.Log(distance);
        if(distance < 2f)
        {
            AiCorpseCheck aiCorpse = alliesAround.enemiesToBeChecked[0].GetComponent<AiCorpseCheck>();
            aiCorpse.corpseWasChecked = true;
            Debug.Log(alliesAround.enemiesToBeChecked[0] + "Aicorpse checked" + aiCorpse.corpseWasChecked);
            return NodeState.SUCCESS;
        }
        agent.SetDestination(alliesAround.enemiesToBeChecked[0].transform.position);
        return NodeState.RUNNING;

        
    }
}
