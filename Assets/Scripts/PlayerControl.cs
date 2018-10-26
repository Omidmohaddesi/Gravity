using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour {

    public float speed = 0.5f;
    public float jumpForce = 3.0f;

    private float h;
    private bool isGrounded = true;
    private Rigidbody rb;
    private Collider coll;
    float disToGround;

    [SerializeField] Gravity gravity;



	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
        coll = GetComponent<Collider>();
        disToGround = coll.bounds.extents.y;
    }
	

    public bool GroundCheck()
    {
        //When gravity is NOT reversed
        if (gravity.isGravityReversed == false)
        {
            if (Physics.Raycast(transform.position, -Vector3.up, disToGround + 0.1f))
            {
                return true;
            }
            else return false;
        }
        else

        //When gravity IS reversed
        if (gravity.isGravityReversed == true)
        {
            if (Physics.Raycast(transform.position, Vector3.up, disToGround + 0.1f))
            {
                return true;
            }
            else return false;
        }

        else return true;

        


    }



    void Update () {
        transform.position += Vector3.right * (Input.GetAxis("Horizontal")) * speed;
    
        //Reverse Gravity
        if (Input.GetKeyDown(KeyCode.Z) && GroundCheck())
        {
            if (gravity.isGravityReversed == false)
            {
                gravity.isGravityReversed = true;
            }
            else if (gravity.isGravityReversed == true)
            {
                gravity.isGravityReversed = false;
            }           
        }


        //Jump
        if (Input.GetKeyDown(KeyCode.Space))
        {


            isGrounded = GroundCheck();

            if (isGrounded)   //If player is in the air, cannot jump
            {
                isGrounded = false;   //Player's jumping now! cannot jump again

                if (gravity.isGravityReversed == false)
                {
                    rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);

                }
                else if (gravity.isGravityReversed == true)
                {
                    rb.AddForce(Vector3.down * jumpForce, ForceMode.Impulse);
                }
            }
            
            
        }
    }

   
}
