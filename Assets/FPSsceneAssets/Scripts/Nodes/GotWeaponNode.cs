using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GotWeaponNode : Node
{
    private AnimatorAI ai;

    public GotWeaponNode(AnimatorAI ai)
    {
        this.ai = ai;
    }

    public override NodeState Evaluate()
    {
        return ai.weaponEquipped ? NodeState.SUCCESS : NodeState.FAILURE;
    }
}
