using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorController : MonoBehaviour
{

    public GameObject downPositon;

    public void OnTriggerStay()
    {
        if (transform.position != downPositon.transform.position) 
            transform.position -= transform.up * Time.deltaTime;
    }

}
