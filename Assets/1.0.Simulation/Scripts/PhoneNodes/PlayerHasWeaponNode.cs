using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHasWeaponNode : Node
{
    public override NodeState Evaluate()
    {
        if (Gun.equippedByPlayer)
        {
            Debug.Log("equipped");
            return NodeState.SUCCESS;
        }
        return NodeState.FAILURE;
    }
}
