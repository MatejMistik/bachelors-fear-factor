using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


/*
***************************************************************************************
*	Title: Dealing damage to AI using hitboxes, ragdolls, and healthbars in Unity [AI #02]
*	Author: TheKiwiCoder
*   Date: 18. 12. 2020
*	Code version: 1.0
*	Availability: https://www.youtube.com/watch?v=oLT4k-lrnwg&ab_channel=TheKiwiCoder
*
***************************************************************************************/
public class Ragdoll : MonoBehaviour

    
{
    Rigidbody[] rigidBodies;
    Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        rigidBodies = GetComponentsInChildren<Rigidbody>();
        animator = GetComponent<Animator>();

        DeactivateRagDoll();
    }


    public void DeactivateRagDoll()
    {
        foreach( var rigidBody in rigidBodies) {
            rigidBody.isKinematic = true;
        }
        animator.enabled = true;
    }

    public void ActivateRagDoll()
    {
        foreach (var rigidBody in rigidBodies)
        {
            rigidBody.isKinematic = false;

        }
        animator.enabled = false;
    }

}
