using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorCheck : MonoBehaviour
{

    public Transform elevatorStandingPosition;
    public Transform doorOpened;
    public Transform doorClosed;
    public GameObject doors;
    public GameObject elevatorFloor;

    void Start()
    {
        doors.transform.position = doorOpened.transform.position;
    }

    public Transform GetElevatorStandingPoint()
    {
        return elevatorStandingPosition;
    }

    public bool IsDoorsOpened()
    {
        return (doors.transform.position == doorOpened.transform.position);
    }

    public void OnCollisionStay()
    {
        
    }
}
