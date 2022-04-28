using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ObserveWhatIsTheProblemNode : Node
{

    NavMeshAgent agent;
    Transform player;
    FearFactorAI fearFactorAI;
    AnimatorAI ai;

    public ObserveWhatIsTheProblemNode(NavMeshAgent agent, Transform player, FearFactorAI fearFactorAI, AnimatorAI ai)
    {
        this.agent = agent;
        this.player = player;
        this.fearFactorAI = fearFactorAI;
        this.ai = ai;
    }

    public override NodeState Evaluate()
    { 
        
        if(fearFactorAI.slider.value > 0.6f){
            Debug.Log("fearValue" + fearFactorAI.slider.value);
            agent.isStopped = false;
            return NodeState.SUCCESS;
        }
        
        agent.isStopped = true;
        agent.transform.LookAt(player);
        if(fearFactorAI.canGainFear)
            fearFactorAI.GainFearOverTime();
        ai.nodeStateText.SetText("Observing Problem");
        return NodeState.FAILURE;

    }
}
