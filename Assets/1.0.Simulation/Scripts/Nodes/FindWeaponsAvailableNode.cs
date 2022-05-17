using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FindWeaponsAvailableNode : Node
{
    private NavMeshAgent agent;
    private Transform WeaponLocation;
    private AiTreeConstructor ai;

    public FindWeaponsAvailableNode(NavMeshAgent agent, Transform weaponLocation, AiTreeConstructor ai)
    {
        this.agent = agent;
        WeaponLocation = weaponLocation;
        this.ai = ai;
    }

    public override NodeState Evaluate()
    {
        Debug.Log("Dest set");
        ai.nodeStateText.SetText("Getting Weapon");
        agent.isStopped = false;
        agent.SetDestination(WeaponLocation.position);
        return NodeState.RUNNING;

    }

}
