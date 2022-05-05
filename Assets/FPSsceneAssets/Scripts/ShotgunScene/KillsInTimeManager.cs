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
    }

    // Update is called once per frame
    void Update()
    {
       
       if(firstKilled)
        {
            timeToGainBonus -= Time.deltaTime;
        }

       if(killsCount >= 3 && !bonusActivated)
        {
            bonusActivated = true;
        }

    }

    public void FirstWasKilled()
    {
        firstKilled = true;
        killsCount++;
    }

}
