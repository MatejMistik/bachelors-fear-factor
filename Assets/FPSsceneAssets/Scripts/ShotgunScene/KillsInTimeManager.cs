using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillsInTimeManager : MonoBehaviour
{

    public float timeToGainBonus;


    public static bool bonusActivated;



    // Start is called before the first frame update
    void Start()
    {
        bonusActivated = false;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(AiHealth.firstKilled);
        //Debug.Log(AiHealth.numberOfAgentsKilled);
       
       if(AiHealth.firstKilled)
        {
            timeToGainBonus -= Time.deltaTime;
        }

       if(AiHealth.numberOfAgentsKilled >= 3 && !bonusActivated && timeToGainBonus >= 0)
        {
            bonusActivated = true;
            Debug.Log("KillsManager" + bonusActivated);
        }


    }

}
