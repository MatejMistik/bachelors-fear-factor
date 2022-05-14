using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Animations.Rigging;
public class WeaponToggle : MonoBehaviour
{
    private Toggle toggle;
    public GameObject weapon;
    private void Start()
    {
        toggle = transform.GetComponent<Toggle>();
        toggle.isOn = true;
        WeaponSelected(toggle);
        toggle.onValueChanged.AddListener(delegate { WeaponSelected(toggle); }); 
    }

    private void WeaponSelected(Toggle toggle)
    {
        AiConstraintsConfig.weapon = !AiConstraintsConfig.weapon;
        Debug.Log(AiConstraintsConfig.weapon);
        weapon.SetActive(AiConstraintsConfig.weapon);
        
    }

    
}
