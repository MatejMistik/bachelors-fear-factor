using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Brackeys mouselook code
/*
***************************************************************************************
*	Title: FIRST PERSON MOVEMENT in Unity - FPS Controller
*	Author: Brackeys
*   Date: 27. 10., 2019
*	Code version: 1.0
*	Availability: https://www.youtube.com/watch?v=_QajrabyTJc&t=1055s&ab_channel=Brackeys
*
***************************************************************************************/

public class MouseLook : MonoBehaviour
{

    [SerializeField] float mouseSensitivity;
    [SerializeField] Transform playerRotation;

    private float xRotation = 0f;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        playerRotation.Rotate(Vector3.up * mouseX);
        
        
    }
}
