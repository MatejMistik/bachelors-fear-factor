using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/***************************************************************************************
*	Title: Unity UI Tutorial Dropdown C# Scripting
*	Author: Jayanam
*   Date: 9. 4. 2020   
*	Code version: 1.0
*	Availability: https://www.youtube.com/watch?v=URS9A4V_yLc&ab_channel=Jayanam
*
***************************************************************************************/

public class DropDownHandling : MonoBehaviour
{
    
    private void Start()
    {
        var array = ChooseDropDown();

        var dropdown = transform.GetComponent<Dropdown>();

        dropdown.options.Clear();

        List<string> items = new List<string>();
        for (int i = 0; i < array.Length; i++)
        {
            items.Add(array[i]);
        }

        foreach (var item in items)
        {
            dropdown.options.Add(new Dropdown.OptionData() { text = item });
        }
        
        DropDownItemSelected(dropdown);

        dropdown.onValueChanged.AddListener(delegate { DropDownItemSelected(dropdown); });
        // One of the components wont show in the menu, quick trick to make it visible.
        dropdown.value = 0;
        dropdown.value = 1;
        dropdown.value = 0;
        
    }

    void DropDownItemSelected(Dropdown dropdown)
    {
       AiConstraintsConfig.chosenTrauma = dropdown.options[dropdown.value].text;
    } 
    
    private  string[] ChooseDropDown()
    {
        return AiConstraintsConfig.childhoodTrauma;
    }
    
}
