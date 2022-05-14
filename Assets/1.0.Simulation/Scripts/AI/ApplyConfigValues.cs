using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplyConfigValues : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        var NPCtransform = transform.GetComponent<Transform>();
        Vector3 scaleChange = new Vector3(AiConstraintsConfig.mass, AiConstraintsConfig.height , AiConstraintsConfig.mass/ 1.2f);
        NPCtransform.transform.localScale = scaleChange;
    }

   
}
