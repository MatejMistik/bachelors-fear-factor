using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SensorToolkit;

public class AlliesAround : MonoBehaviour
{
    public Sensor sensor;
    private int numberOfEnemies;
    public GameObject[] deadEnemies { get; private set; }
    public GameObject[] liveEnemies { get; private set; }

    public List<GameObject> enemiesToBeChecked = new List<GameObject>();

    public GameObject[] EnemiesNearbyAlive()
    {
        List<GameObject> liveEnemiesList = new List<GameObject>();
        int alliesAliveCounter = 0;
        foreach (GameObject detectedObject in sensor.DetectedObjects)
        {
            if (detectedObject.name == "Enemy" && detectedObject.CompareTag("Enemy"))
            {
                alliesAliveCounter++;
                liveEnemiesList.Add(detectedObject);
            }
            liveEnemies = liveEnemiesList.ToArray();
        }

        return liveEnemies;
    }

    public GameObject[] EnemiesNearbyDead()
    {
        List<GameObject> deadEnemiesList = new List<GameObject>();
        int alliesDeadCounter = 0;
        foreach (GameObject detectedObject in sensor.DetectedObjects)
        {
            if (detectedObject.name == "Enemy" && detectedObject.CompareTag("DeadEnemy"))
            {
                alliesDeadCounter++;
                deadEnemiesList.Add(detectedObject);
            }

        }
        deadEnemies = deadEnemiesList.ToArray();

        return deadEnemies;
    }

    
}
