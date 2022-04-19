using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveWeapon : MonoBehaviour
{

    RayCastWeapon weapon;
    public Transform weaponParent;
    AnimatorAI ai;
    // Start is called before the first frame update
    void Start()
    {
        ai = GetComponent<AnimatorAI>();
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
