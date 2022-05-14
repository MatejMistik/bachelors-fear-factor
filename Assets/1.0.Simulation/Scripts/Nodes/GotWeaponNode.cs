using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GotWeaponNode : Node
{
    private AiTreeConstructor ai;

    public GotWeaponNode(AiTreeConstructor ai)
    {
        this.ai = ai;
    }

    public override NodeState Evaluate()
    {
        return ai.weaponEquipped ? NodeState.SUCCESS : NodeState.FAILURE;
    }
}
