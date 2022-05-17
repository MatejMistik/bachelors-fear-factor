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

public class WeaponPickup : MonoBehaviour
{
    ActiveWeapon activeWeapon;
    public RayCastWeapon weaponPrefab;
    private bool weaponPicked;
    // Start is called before the first frame update

    private void Start()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        activeWeapon = other.gameObject.GetComponentInParent<ActiveWeapon>();
        if (activeWeapon && !weaponPicked)
        {
            weaponPicked = true;
            RayCastWeapon newWeapon = Instantiate(weaponPrefab);
            activeWeapon.Equip(newWeapon);
            Invoke(nameof(ResetWeaponPicked), 1f);
           
        }
    }

    private void ResetWeaponPicked()
    {
        weaponPicked = false;
    }
}
