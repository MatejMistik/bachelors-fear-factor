using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SexToogles : MonoBehaviour
{
    public Toggle femaleToggle;
    public Toggle maleToggle;
    public GameObject Enemy;
    private SkinnedMeshRenderer SkinnedMeshRenderer;
    private void Start()
    {
        SkinnedMeshRenderer = Enemy.GetComponentInChildren<SkinnedMeshRenderer>();
        SexSelectedMale(maleToggle);
        maleToggle.onValueChanged.AddListener(delegate { SexSelectedMale(maleToggle); });
        femaleToggle.onValueChanged.AddListener(delegate { SexSelectedFemale(femaleToggle); });
    }

    private void SexSelectedMale(Toggle toggle)
    {
        if (maleToggle.isOn)
        {
            AiConstraintsConfig.female = false;
            AiConstraintsConfig.male = true;
            femaleToggle.isOn = false;
            SkinnedMeshRenderer.material.color = Color.blue;
        }
        else
        {
            AiConstraintsConfig.male = false;
            SkinnedMeshRenderer.material.color = Color.white;
        }
    }

    private void SexSelectedFemale(Toggle toggle)
    {
        if (femaleToggle.isOn)
        {
            AiConstraintsConfig.male = false;
            AiConstraintsConfig.female = true;
            maleToggle.isOn = false;
            SkinnedMeshRenderer.material.color = Color.red;
        }
        else
        {
            AiConstraintsConfig.female = false;
            SkinnedMeshRenderer.material.color = Color.white;
        }

    }


}
