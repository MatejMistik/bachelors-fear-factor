using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPScontroller : MonoBehaviour
{
    [SerializeField] int speed = 6;
    private Rigidbody rigidbodyComponent;
    private float horizontalInput;
    private float verticalInput;
    private bool jumpKeyWasPressed;

    void Start()
    {
        rigidbodyComponent = GetComponent<Rigidbody>();

    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Space))
        {
            jumpKeyWasPressed = true;
        }

     horizontalInput = Input.GetAxis("Horizontal");    
     verticalInput = Input.GetAxis("Vertical");       
    }

    private void FixedUpdate()
    {
        //need to actualise velocity of y because it is slowing down gravity
        rigidbodyComponent.linearVelocity = new Vector3(horizontalInput * speed, rigidbodyComponent.linearVelocity.y, verticalInput * speed);

        if (jumpKeyWasPressed)
        {
            rigidbodyComponent.AddForce(Vector3.up * 5, ForceMode.VelocityChange);
            jumpKeyWasPressed = false;
        }
    }

    }
