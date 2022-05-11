using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/*
***************************************************************************************
*	Title: AI in Unity Turorial. Behavior Trees.
*	Author: GameDevChef
*   Date: 22.05., 2022
*	Code version: 1.0
*	Availability: https://github.com/GameDevChef/BehaviourTrees
*
***************************************************************************************/

public class ShootingNode : Node
{
    public NavMeshAgent agent;
    private AiTreeConstructor ai;
    private Transform playerTransform;
    public RayCastWeapon weapon;

    public ShootingNode(NavMeshAgent agent, AiTreeConstructor ai, Transform playerTransform, RayCastWeapon weapon)
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
        agent.transform.LookAt(playerTransform);
        weapon.PrepareToShoot();
        return NodeState.RUNNING;
    }

}
