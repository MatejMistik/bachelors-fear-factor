using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ShootingNode : Node
{
    public NavMeshAgent agent;
    private AnimatorAI ai;
    private Transform playerTransform;
    public RayCastWeapon weapon;

    public ShootingNode(NavMeshAgent agent, AnimatorAI ai, Transform playerTransform, RayCastWeapon weapon)
    {
        this.agent = agent;
        this.ai = ai;
        this.playerTransform = playerTransform;
        this.weapon = weapon;
    }

    public override NodeState Evaluate()
    {
        if (weapon == null)
        {
            weapon = ai.weapon;
        }
        agent.isStopped = true;
        ai.DebugMessage("Shooting");
        agent.transform.LookAt(playerTransform);
        weapon.PrepareToShoot();
        return NodeState.RUNNING;
    }

}
