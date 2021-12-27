using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerModel : MonoBehaviour
{
    //movement 

    private bool jumpKeyWassPressed;
    private float horizontalInput;
    private Rigidbody rigidbodyComponent;
    private bool isGrounded;
    private int jumpMax = 2;
    private int jumpCounter;
    private int heightOfJump = 5;
    private int superJumpsRemaining;
    public static int playerLife;

    // Start is called before the first frame update
    void Start()
    {
        rigidbodyComponent = GetComponent<Rigidbody>();

    }

    // Update is called once per frame
    void Update()
    {


        if(Input.GetKeyDown(KeyCode.Space))
        {
            jumpKeyWassPressed = true;
        }

        horizontalInput = Input.GetAxis("Horizontal");
 
    }

    // FixedUpdate is call once every physics update / 100hz
    private void FixedUpdate()
    {
        //need to actualise velocity of y because it is slowing down gravity
        rigidbodyComponent.velocity = new Vector3(horizontalInput * 8, rigidbodyComponent.velocity.y, 0);

        if (!isGrounded && jumpCounter == jumpMax)
        {
            jumpKeyWassPressed = false;
            return;
            
        }

        if (jumpKeyWassPressed)
        {
            float jumpPower = 0f;
            if(superJumpsRemaining > 0)
            {
                jumpPower = 2f; 
            }
            rigidbodyComponent.AddForce(Vector3.up * (heightOfJump + jumpPower), ForceMode.VelocityChange);
            jumpKeyWassPressed = false;
            jumpCounter++;
            heightOfJump = heightOfJump * 1;
        }


    }

    private void OnCollisionEnter(Collision collision)
    {
        heightOfJump = 5;
        isGrounded = true;
        jumpCounter = 0;
    }

    private void OnCollisionExit(Collision collision)
    {
        isGrounded = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == 3)
        {     
            Destroy(other.gameObject);
            ScoreCounter.scoreValue += 100;
            superJumpsRemaining++;
        }

        if (other.gameObject.layer == 7)
        {
            Destroy(other.gameObject);
            LifeCounter.lifeValue += 1;
            playerLife++;

        }
    }




}
