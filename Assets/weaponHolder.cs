using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weaponHolder : MonoBehaviour
{
    public GameObject rifle;
    public GameObject shotgun;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.H) && ( rifle.activeInHierarchy || shotgun.activeInHierarchy))
        {
            Gun.equippedByPlayer = false;
            rifle.SetActive(false);
            shotgun.SetActive(false);

        }

        if (Input.GetKey(KeyCode.Alpha1))
        {
            Gun.equippedByPlayer = true;
            shotgun.SetActive(false);
            rifle.SetActive(true);
            
        }

        if (Input.GetKey(KeyCode.Alpha2))
        {
            rifle.SetActive(false);
            shotgun.SetActive(true);
            
        }

    }
}
