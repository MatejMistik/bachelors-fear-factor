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
        agent.SetDestination(alliesAround.enemiesToBeChecked[0].transform.position);
        return NodeState.RUNNING;
    }
}
