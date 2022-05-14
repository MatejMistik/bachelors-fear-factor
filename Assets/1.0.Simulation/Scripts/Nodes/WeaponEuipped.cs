using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponEuipped : Node
{

    private AiTreeConstructor ai;

    public WeaponEuipped(AiTreeConstructor ai)
    {
        this.ai = ai;
    }

    public override NodeState Evaluate()
    {
        if (!ai.weaponEquipped)
        {
            return NodeState.SUCCESS;
        }
        return NodeState.FAILURE;
    }
}
