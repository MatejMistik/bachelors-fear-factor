using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/***************************************************************************************
*	Title: How to aim a weapon using inverse kinematics in Unity [AI #05]
*	Author: TheKiwiCoder
*   Date: 24. 1. 2021   
*	Code version: 1.0
*	Availability: https://www.youtube.com/watch?v=Q56quIB2sOg&t=6s&ab_channel=TheKiwiCoder
*
***************************************************************************************/

public class weaponIK : MonoBehaviour
{
    public Transform target;
    public Transform bulletHole;
    public Transform bone;
    public int iterations = 10;
    // Start is called before the first frame update
    private void Awake()
    {
        enabled = false;
    }
    void Start()
    {
        bulletHole = GameObject.Find("BulletHole").GetComponent<Transform>();
        Debug.Log(bulletHole);
    }


    private void LateUpdate()
    {
        Vector3 targetPosition = target.position;
        for (int i = 0; i < iterations; i++)
        {
            AimAtTarget(bone, targetPosition);
        }
       
    }

    public void AimAtTarget(Transform bone, Vector3 targetPosition)
    {
        Vector3 aimDirection = bulletHole.forward;
        Vector3 targetDirection = targetPosition - bulletHole.position;
        Quaternion aimTowards = Quaternion.FromToRotation(aimDirection, targetDirection);
        bone.rotation = aimTowards * bone.rotation;

    }
}
