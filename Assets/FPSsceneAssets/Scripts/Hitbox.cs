using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
