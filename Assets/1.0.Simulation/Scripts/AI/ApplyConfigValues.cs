using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ApplyConfigValues : MonoBehaviour
{

    // Later these constraints value could be editable in Confugator
    
    // Start is called before the first frame update
    void Awake()
    {
        var NPCtransform = transform.GetComponent<Transform>();
        Vector3 scaleChange = new Vector3(AiConstraintsConfig.mass, AiConstraintsConfig.height , AiConstraintsConfig.mass/ 1.2f);
        NPCtransform.transform.localScale = scaleChange;

        if(SceneManager.GetActiveScene().name == "DeadBodyScene")
        {
            if (AiConstraintsConfig.chosenTrauma == "SeenDeadBody") FearFactorAI.traumaMultiplier = 0.8f;
            return;
            
        }
        if (SceneManager.GetActiveScene().name == "ElevatorScene" || SceneManager.GetActiveScene().name == "ConfrontationScene")
        {
            if (AiConstraintsConfig.chosenTrauma == "BeenInShooting") FearFactorAI.traumaMultiplier = 1.5f;
            return;

        }
        if (SceneManager.GetActiveScene().name == "TailGating")
        {
            Debug.Log(AiConstraintsConfig.chosenTrauma + "trauma");
            Debug.Log("SceneNameTailgating");
            if (AiConstraintsConfig.chosenTrauma == "BeenTailgated") 
            {
                FearFactorAI.traumaMultiplier = 1.8f;
                Debug.Log("Value Changed");
            }   
            return;
        }


    }

   
}
