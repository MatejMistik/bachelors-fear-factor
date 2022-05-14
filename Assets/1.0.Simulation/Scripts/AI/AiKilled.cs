using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiKilled : MonoBehaviour
{
    AiHealth aiHealth;

    public bool aiKilled = false;
    public float killedTimer;
    private bool disableScript;

    // Start is called before the first frame update
    void Start()
    {
         
    }

    // Update is called once per frame
    void Update()
    {
        if (aiKilled && killedTimer >= 0 )
        {
            killedTimer -= Time.deltaTime;
        }
    }
}
