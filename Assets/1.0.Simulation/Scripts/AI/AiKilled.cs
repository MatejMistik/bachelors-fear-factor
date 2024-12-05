using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiKilled : MonoBehaviour
{

    public bool aiKilled = false;
    public float killedTimer;

    // Update is called once per frame
    void Update()
    {
        if (aiKilled && killedTimer >= 0 )
        {
            killedTimer -= Time.deltaTime;
        }
    }
}
