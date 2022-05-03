using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
class PlayerMovement : MonoBehaviour
{
    [SerializeField] float dashLength = 0.12f;
    [SerializeField] float dashSpeed = 120f;
    [SerializeField] float dashResetTime = 0.2f;
    [SerializeField] CharacterController controller;
    [SerializeField] float speed = 8f;
    [SerializeField] float gravity = -9.81f;
    [SerializeField] Transform groundCheck;
    [SerializeField] float groundDistance = 0.4f;
    [SerializeField] LayerMask groundMask;
    [SerializeField] float jumpHeight = 4f;
    [SerializeField] float sprintSpeed = 2f;
    [SerializeField] float ultraSpeedTime = 4f;
    [SerializeField] float coinPickupSpeedMultiplier = 1f;
    

    private float ultraSpeedTimeRemaining;
    private float SprintSpeedMultiplier = 1f;
    private bool isGrounded;
    private float horizontalInput;
    private float verticalInput;
    private Vector3 dashMove;
    private float dashing = 0f;
    private float dashingTime = 0f;
    private bool canDash = true;
    private bool dashingNow = false;
    private bool dashReset = true;
    private Vector3 velocity;
    private float superJump = 1f;

    // Update is called once per frame
    void Update()
    {

        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        // Prevents from overloading of velocity.y because of Gravity Force
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        if (Input.GetKey(KeyCode.LeftShift) && isGrounded)
        {
            SprintSpeedMultiplier = sprintSpeed;

        }
        else
        {
            SprintSpeedMultiplier = 1f;
        }

        if (ultraSpeedTimeRemaining >= 0)
        {
            ultraSpeedTimeRemaining -= Time.deltaTime;
        }
        else
        {
            coinPickupSpeedMultiplier = 1f;
        }

        Vector3 move = transform.right * horizontalInput + transform.forward * verticalInput;
        controller.Move(coinPickupSpeedMultiplier * speed * SprintSpeedMultiplier * Time.deltaTime * move);

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity * superJump);
            superJump = 1f;
        }

        velocity.y += gravity * Time.deltaTime;
        //nDebug.Log("velo" + velocity.y);
        

        controller.Move(velocity * Time.deltaTime);


        
        if (move.magnitude > 1)
        {
            move = move.normalized;
        }


        // Dashing taken from NET , MINE implented, but need some tweaking still. This one is hardly optimized for multiple dashes.
        if (Input.GetKeyDown(KeyCode.LeftControl) == true && dashing < dashLength && dashingTime < dashResetTime && dashReset == true && canDash == true)
        {
            dashMove = move;
            canDash = false;
            dashReset = false;
            dashingNow = true;
        }

        if (dashingNow == true && dashing < dashLength)
        {
            _ = controller.Move(dashSpeed * Time.deltaTime * dashMove);
            dashing += Time.deltaTime;
        }

        if (dashing >= dashLength)
        {
            dashingNow = false;
        }

        if (dashingNow == false)
        {
            controller.Move(speed * Time.deltaTime * move);
        }

        if (dashReset == false)
        {
            dashingTime += Time.deltaTime;
        }

        if (controller.isGrounded && canDash == false && dashing >= dashLength)
        {
            canDash = true;
            dashing = 0f;
        }

        if (dashingTime >= dashResetTime && dashReset == false)
        {
            dashReset = true;
            dashingTime = 0f;
        }


    }

    private void OnTriggerEnter(Collider other)
    {
        // On Coinpickup Player gains Ultraspeed for 5 sec.
        if (other.gameObject.layer == 3)
        {
            coinPickupSpeedMultiplier = 5f;
            ultraSpeedTimeRemaining = ultraSpeedTime;
            Destroy(other.gameObject);
        }

        if (other.gameObject.layer == 9)
        {
            superJump = 4f;
        }

        if (other.gameObject.layer == 7)
        {
            LifeScript.Lives += 1;
            Destroy(other.gameObject);  
        }

    }

}
