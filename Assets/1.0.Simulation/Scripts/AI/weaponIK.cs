using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/***************************************************************************************
*	Title: How to aim a weapon using inverse kinematics in Unity [AI #05]
*	Author: TheKiwiCoder
*   Date: 24. 1. 2021   
*	Code version: 1.0
*	Availability: https://www.youtube.com/watch?v=Q56quIB2sOg&t=6s&ab_channel=TheKiwiCoder
*	Functions LateUdate, AimAtTarget
*
***************************************************************************************/

public class weaponIK : MonoBehaviour
{
    [System.Serializable]
    public class HumanBone
    {
        public HumanBodyBones bone;
        public float weight = 1.0f;

    }

   

    public Transform target;
    private Transform bulletHole;
    public RayCastWeapon rayCastWeapon;
    public int iterations = 10;
    [Range(0, 1)]
    public float weight = 1.0f;

    public HumanBone[] humanBones;
    Transform[] boneTransforms;

    private void Start()
    {
        if(rayCastWeapon) bulletHole = rayCastWeapon.bulletHole;

        Animator animator = GetComponent<Animator>();
        boneTransforms = new Transform[humanBones.Length];
        for (int i = 0; i < boneTransforms.Length; i++)
        {
            boneTransforms[i] = animator.GetBoneTransform(humanBones[i].bone);
        }
        

    }

    private void LateUpdate()
    {
        if (enabled)
        {

        
        Vector3 targetPosition = target.position;
        for (int i = 0; i < iterations; i++)
        {
            for (int b = 0; b < boneTransforms.Length; b++)
            {
                Transform bone = boneTransforms[b];
                float boneWeight = humanBones[b].weight * weight;
                AimAtTarget(bone, targetPosition, boneWeight);
            }
            
        }
        }

    }

    public void AimAtTarget(Transform bone, Vector3 targetPosition, float weight)
    {
        Vector3 aimDirection = bulletHole.forward;
        Vector3 targetDirection = targetPosition - bulletHole.position;
        Quaternion aimTowards = Quaternion.FromToRotation(aimDirection, targetDirection);
        Quaternion blendedRotation = Quaternion.Slerp(Quaternion.identity, aimTowards, weight);
        bone.rotation = blendedRotation * bone.rotation;

    }

    public void AssignBulletHole(RayCastWeapon weapon)
    {
        rayCastWeapon = weapon;
        bulletHole = rayCastWeapon.bulletHole;
        weight = 1.0f;
    }
}
