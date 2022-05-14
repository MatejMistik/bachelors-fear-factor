using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
***************************************************************************************
*	Title: Dealing damage to AI using hitboxes, ragdolls, and healthbars in Unity [AI #02]
*	Author: TheKiwiCoder
*   Date: 18. 12. 2020
*	Code version: 1.0
*	Availability: https://www.youtube.com/watch?v=oLT4k-lrnwg&ab_channel=TheKiwiCoder
*
***************************************************************************************/

public class Hitbox : MonoBehaviour
{
    public AiTreeConstructor ai;
    public AiHealth aiHealth;
    

    public void OnRaycasthit(Gun weapon)
    {
        aiHealth.TakeDamage(weapon.damage);
    }

    public void OnRaycasthit(RayCastWeapon rayCastWeapon)
    {
        return;
    }
}
