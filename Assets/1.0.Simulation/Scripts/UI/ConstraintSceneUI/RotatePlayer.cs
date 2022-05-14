using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RotatePlayer : MonoBehaviour
{
    
    private Slider rotationSlider;
    public Transform NPCtransform;
    // Start is called before the first frame update
    private void Start()
    {
        rotationSlider = GameObject.Find("PlayerRotationSlider").GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        CalcRotation();
        Debug.Log(rotationSlider.value);

        
        
    }

    void CalcRotation()
    {
        NPCtransform.transform.rotation = Quaternion.Euler(0, rotationSlider.value, 0);
        Debug.Log(NPCtransform.transform.rotation);
    }
}
