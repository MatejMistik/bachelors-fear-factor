using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillsInTimeManager : MonoBehaviour
{

    public float timeToGainBonus;

    private float timeSinceFirstKilled;
    private bool firstKilled;
    private bool bonusActivated;
    private int killsCount;


    // Start is called before the first frame update
    void Start()
    {
        firstKilled = false;
        killsCount = 0;
        bonusActivated = false;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(AiHealth.firstkilled);
        //Debug.Log(AiHealth.numberOfAgentsKilled);
       
       if(AiHealth.firstkilled)
        {
            timeToGainBonus -= Time.deltaTime;
        }

       if(AiHealth.numberOfAgentsKilled >= 3 && !bonusActivated && timeToGainBonus >= 0)
        {
            bonusActivated = true;
            Debug.Log("KillsManager" + bonusActivated);
        }

    }

    public void FirstWasKilled()
    {
        firstKilled = true;
        killsCount++;
    }

}
