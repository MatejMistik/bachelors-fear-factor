using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandsUp : MonoBehaviour
{
    Animator animator;
    private Transform handsDownLeft;
    private Transform handsDownRight;
    private Transform handsUpLeft;
    private Transform handsUpRight;
    private GameObject leftArm;
    private GameObject rightArm;
    private Rigidbody rightArmRigidbody;
    private Rigidbody leftArmRigidbody;
    private bool handAreUp = false;

    public Camera fpsCam;
    [SerializeField] float range = 100f;
    // Start is called before the first frame update
    void Start()
    {
        handsUpLeft = transform.Find("Skeleton/Hips/Spine/Chest/UpperChest/Left_Shoulder/Left_UpperArm/HandPositionLeftUp").GetComponent<Transform>();
        handsUpRight = transform.Find("Skeleton/Hips/Spine/Chest/UpperChest/Right_Shoulder/Right_UpperArm/HandPositionRightUp").GetComponent<Transform>();
        handsDownRight = transform.Find("Skeleton/Hips/Spine/Chest/UpperChest/Right_Shoulder/Right_UpperArm/HandPosRightDown").GetComponent<Transform>();
        rightArm = GameObject.Find("Skeleton/Hips/Spine/Chest/UpperChest/Right_Shoulder/Right_UpperArm");
        handsDownLeft = transform.Find("Skeleton/Hips/Spine/Chest/UpperChest/Left_Shoulder/Left_UpperArm/HandPosLeftDown").GetComponent<Transform>();
        leftArm = GameObject.Find("Skeleton/Hips/Spine/Chest/UpperChest/Left_Shoulder/Left_UpperArm");
        rightArmRigidbody = rightArm.GetComponent<Rigidbody>();
        leftArmRigidbody = leftArm.GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

        Debug.Log(handsUpLeft);
        Debug.Log(rightArm);

        if (Input.GetKeyDown(KeyCode.Mouse1) || Input.GetKey(KeyCode.Mouse1))
        {
            Debug.Log("right mouse clicked");
            if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out RaycastHit hit, range))
            {
                var hitBox = hit.collider.GetComponent<Hitbox>();
                Debug.Log(hitBox);
                if (hitBox && !handAreUp)
                {
                    handAreUp = true;
                    animator.enabled = false;

                    Vector3 eulerRotation = new Vector3(handsUpRight.eulerAngles.x, handsDownLeft.eulerAngles.y, handsUpRight.eulerAngles.z);
                    rightArm.transform.rotation = Quaternion.Euler(eulerRotation);
                    Debug.Log("bef" + handsUpRight.eulerAngles.x);
                    rightArm.transform.rotation = Quaternion.Euler(handsUpRight.eulerAngles.x, handsUpRight.eulerAngles.y, handsUpRight.eulerAngles.z);
                    leftArm.transform.rotation = Quaternion.Euler(handsUpLeft.eulerAngles.x, handsUpLeft.eulerAngles.y, handsUpLeft.eulerAngles.z);
                }
                /*
                else
                {
                    handAreUp = false;
                    rightArm.transform.rotation = Quaternion.Euler(handsDownRight.rotation.x*180f, handsDownRight.rotation.y*180f, handsDownRight.rotation.z*180f);
                    leftArm.transform.rotation = Quaternion.Euler(handsDownLeft.rotation.x*180f, handsDownLeft.rotation.y*180f, handsDownLeft.rotation.z*180f);
                    animator.enabled = true;
                }*/

            }
        }
    }
}
