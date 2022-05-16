using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIWeaponNotEuipped : Node
{

    private AiTreeConstructor ai;

    public AIWeaponNotEuipped(AiTreeConstructor ai)
    {
        this.ai = ai;
    }

    public override NodeState Evaluate()
    {
        if (!ai.weaponEquipped)
        {
            //Debug.Log("no weapon");
            return NodeState.SUCCESS;
        }
        return NodeState.FAILURE;
    }
}
