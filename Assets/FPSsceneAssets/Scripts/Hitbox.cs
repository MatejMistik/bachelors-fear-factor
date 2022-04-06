using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hitbox : MonoBehaviour
{
    public AnimatorAI ai;
    public NewEnemyHealth health; 
   
    public void OnRaycasthit(Gun weapon)
    {
        health.TakeDamage(weapon.damage);
    }
}
