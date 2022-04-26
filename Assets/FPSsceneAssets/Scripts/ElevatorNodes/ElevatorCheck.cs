using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorCheck : MonoBehaviour
{

    public Transform elevatorStandingPosition;
    public Transform doorsOpenedPosition;
    public Transform doorsClosedPosition;
    public GameObject doors;
    public GameObject elevatorFloor;
    public bool doorsOpened;
    private float closingPointDistance;
    public float moveSpeed;
    public bool doorsAreMoving = false;

    private void Update()
    {
        Debug.Log("doorsAreMoving" + doorsAreMoving);
        if (doorsOpened)
        {
            if (doorsAreMoving && doors.transform.position != doorsClosedPosition.transform.position)
            {
                CloseElevatorDoors();
            }
            if (doors.transform.position == doorsClosedPosition.transform.position)
            {
                doorsAreMoving = false;
                doorsOpened = false;
            }
        } else
        {
            if (doorsAreMoving && doors.transform.position != doorsOpenedPosition.transform.position)
            {
                OpenElevatorDoors();
            }
            if (doors.transform.position == doorsClosedPosition.transform.position)
            {
                doorsAreMoving = false;
                doorsOpened = true;
            }
        }
        

        


    }


    void Start()
    {
        doors.transform.position = doorsOpenedPosition.transform.position;
        doorsOpened = true;
    }

    public Transform GetElevatorStandingPoint()
    {
        return elevatorStandingPosition;
    }

    public bool IsDoorsOpened()
    {
        return (doors.transform.position == doorsOpenedPosition.transform.position);
    }

    public void CloseElevatorDoors()
    {
        doors.transform.position = Vector3.MoveTowards(doors.transform.position, doorsClosedPosition.transform.position, moveSpeed * Time.deltaTime);
    }

    public void OpenElevatorDoors()
    {
        doors.transform.position = Vector3.MoveTowards(doors.transform.position, doorsOpenedPosition.transform.position, moveSpeed * Time.deltaTime);
    }

}
