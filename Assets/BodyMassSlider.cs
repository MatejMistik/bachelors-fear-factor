using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BodyMassSlider : MonoBehaviour
{
    public TextMeshProUGUI textMass;
    public TextMeshProUGUI textHeight;
    private Slider massSlider;
    private Slider heightSlider;
    public Transform NPCtransform;
    private float startingHeight = 1.8f;
    private float startingMass = 2f;
    // Start is called before the first frame update
    private void Awake()
    {
        //textHeight = GameObject.Find("HeightValueText").GetComponent<TextMeshProUGUI>();
        massSlider = GameObject.Find("BodyMassSlider").GetComponent<Slider>();
        heightSlider = GameObject.Find("HeightSlider").GetComponent<Slider>();
        massSlider.value = startingMass;
        heightSlider.value = startingHeight;
    }

   

    // Update is called once per frame
    void Update()
    {
        CalcBodyProportions();
        SetText();
        Debug.Log(massSlider.value);
        Debug.Log(heightSlider.value);



    }

    void CalcBodyProportions()
    {
        Vector3 scaleChange = new Vector3(massSlider.value, heightSlider.value, massSlider.value/1.2f);
        NPCtransform.transform.localScale = scaleChange;
        AiConstraintsConfig.height = heightSlider.value;
        AiConstraintsConfig.mass = massSlider.value;

    }

    void SetText()
    {
        textHeight.SetText(heightSlider.value.ToString("0.00") + "m");
        textMass.SetText(massSlider.value.ToString("0.00"));
    }
}
