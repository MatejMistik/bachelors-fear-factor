using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FindWeaponsAvailableNode : Node
{
    private WeaponPickup weaponPickup;
    private NavMeshAgent agent;
    private Transform WeaponLocation;
    private AnimatorAI ai;

    public FindWeaponsAvailableNode(WeaponPickup weaponPickup, NavMeshAgent agent, Transform weaponLocation, AnimatorAI ai)
    {
        this.weaponPickup = weaponPickup;
        this.agent = agent;
        WeaponLocation = weaponLocation;
        this.ai = ai;
    }

    public override NodeState Evaluate()
    {
        
        agent.SetDestination(WeaponLocation.position);
        return NodeState.RUNNING;

    }

}
