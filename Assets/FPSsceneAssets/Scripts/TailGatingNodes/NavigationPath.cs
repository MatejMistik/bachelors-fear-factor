using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavigationPath : MonoBehaviour
{
    public Transform[] waypoints;
    private int waypointIndex;
    private float distance;
    public NavMeshAgent agent;

     void Start()
    {
        waypointIndex = 0;
        agent.transform.LookAt(waypoints[waypointIndex].position);
        agent.SetDestination(waypoints[waypointIndex].position);
    }

    private void Update()
    {
        distance = Vector3.Distance(agent.transform.position, waypoints[waypointIndex].position);
        if(distance < 5f)
        {
            IncreaseIndex();
        }
        
    }

    public void IncreaseIndex()
    {
        waypointIndex++;
        if(waypointIndex > waypoints.Length)
        {
            waypointIndex = 0;
        }

        agent.transform.LookAt(waypoints[waypointIndex].position);
        agent.SetDestination(waypoints[waypointIndex].position);
        
    }
}
