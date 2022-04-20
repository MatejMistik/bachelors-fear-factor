using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ObserveWhatIsTheProblemNode : Node
{

    NavMeshAgent agent;
    Transform player;
    FearFactorAI fearFactorAI;

    public ObserveWhatIsTheProblemNode(NavMeshAgent agent, Transform player, FearFactorAI fearFactorAI)
    {
        this.agent = agent;
        this.player = player;
        this.fearFactorAI = fearFactorAI;
    }

    public override NodeState Evaluate()
    {
        if(fearFactorAI.slider.value > 0.6f){
            agent.isStopped = false;
            return NodeState.SUCCESS;
        }
        agent.isStopped = true;
        agent.transform.LookAt(player);
        if(fearFactorAI.canGainFear)
            fearFactorAI.GainFearOverTime();
        return NodeState.RUNNING;

    }
}
