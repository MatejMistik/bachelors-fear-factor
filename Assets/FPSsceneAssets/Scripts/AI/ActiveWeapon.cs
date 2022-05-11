using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/***************************************************************************************
*	Title: Equip weapons using Mesh Sockets and Animation Rigging in Unity [AI #04]
*	Author: TheKiwiCoder
*   Date: 17. 1. 2021   
*	Code version: 1.0
*	Availability: https://www.youtube.com/watch?v=_c-r1S4PZb4&list=PLyBYG1JGBcd009lc1ZfX9ZN5oVUW7AFVy&index=4&ab_channel=TheKiwiCoder
*
***************************************************************************************/

public class ActiveWeapon : MonoBehaviour
{

    RayCastWeapon weapon;
    public Transform weaponParent;
    AiTreeConstructor ai;
    // Start is called before the first frame update
    void Start()
    {
        ai = GetComponent<AiTreeConstructor>();
        RayCastWeapon existingWeapon = GetComponentInChildren<RayCastWeapon>();
        if (existingWeapon)
        {
            Equip(existingWeapon);
        }
        
    }


    public void Equip(RayCastWeapon newWeapon)
    {
                
        weapon = newWeapon;
        weapon.transform.parent = weaponParent;
        weapon.transform.localPosition = Vector3.zero;
        weapon.transform.localRotation = Quaternion.identity;
        Debug.Log("Active weapon script " + weapon);
        ai.EquipWeapon();
        ai.weaponEquipped = true;
        

    }
}
