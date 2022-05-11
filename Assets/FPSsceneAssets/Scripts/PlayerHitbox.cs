using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class PlayerHitbox : MonoBehaviour
{
    public LifeScript playerLifeScript;


    public void OnRaycasthit(RayCastWeapon weapon)
    {
        playerLifeScript.TakeDamage(weapon.damage);
    }

}

