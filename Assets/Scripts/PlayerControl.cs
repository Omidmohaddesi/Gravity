using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{

    public float speed = 0.5f;
    public float jumpForce = 3.0f;
    public float bulletSratingSpeed = 5.0f;

    private float h;
    private bool isGrounded = true;
    private Rigidbody rigidBody;
    private Collider collider;
    float disToGround;
    public GameObject bullet;
    public Transform CharacterSprite;
    public float bulletForce = 100.0f;
    public GameObject FirePos;

    public Animator Character_animator;

    [SerializeField] Gravity gravity;

    enum Direction { left, right };
    Direction direction = Direction.right;


    // Use this for initialization
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        collider = GetComponent<Collider>();
        disToGround = collider.bounds.extents.y;
    }


    public bool GroundCheck()
    {
        //When gravity is NOT reversed
        if (gravity.gravityState == Gravity.gravity.normal)
        {
            if (Physics.Raycast(transform.position, -Vector3.up, disToGround + 0.1f))
            {
                return true;
            }
            else return false;
        }
        else

        //When gravity IS reversed
        if (gravity.gravityState == Gravity.gravity.reversed)
        {
            if (Physics.Raycast(transform.position, Vector3.up, disToGround + 0.1f))
            {
                return true;
            }
            else return false;
        }

        else return true;
    }



    void Update()
    {
        transform.position += Vector3.right * (Input.GetAxis("Horizontal")) * speed;
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            direction = Direction.left;
            if (gravity.gravityState == Gravity.gravity.normal) // updating sprite direction *Frank
            {
                CharacterSprite.localScale = new Vector3(1, CharacterSprite.localScale.y, CharacterSprite.localScale.z);
            }
            else
            {
                CharacterSprite.localScale = new Vector3(-1, CharacterSprite.localScale.y, CharacterSprite.localScale.z);
            }

        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            direction = Direction.right;
            if (gravity.gravityState == Gravity.gravity.normal) // updating sprite direction *Frank
            {
                CharacterSprite.localScale = new Vector3(-1, CharacterSprite.localScale.y, CharacterSprite.localScale.z);
            }
            else
            {
                CharacterSprite.localScale = new Vector3(1, CharacterSprite.localScale.y, CharacterSprite.localScale.z);
            }
        }
        //Reverse Gravity
        if (Input.GetKeyDown(KeyCode.Z) && GroundCheck())
        {
            if (gravity.gravityState == Gravity.gravity.normal)
            {
                Character_animator.SetBool("Character_reversed", true); // checking for reverse animation *Frank
                gravity.gravityState = Gravity.gravity.reversed;
            }
            else if (gravity.gravityState == Gravity.gravity.reversed)
            {
                Character_animator.SetBool("Character_reversed", false); // checking for reverse animation *Frank
                gravity.gravityState = Gravity.gravity.normal;
            }
        }


        //Jump
        if (Input.GetKeyDown(KeyCode.Space))
        {


            isGrounded = GroundCheck();

            if (isGrounded)   //If player is in the air, cannot jump
            {
                isGrounded = false;   //Player's jumping now! cannot jump again

                if (gravity.gravityState == Gravity.gravity.normal)
                {
                    rigidBody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);

                }
                else if (gravity.gravityState == Gravity.gravity.reversed)
                {
                    rigidBody.AddForce(Vector3.down * jumpForce, ForceMode.Impulse);
                }
            }


        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            //spawn bullet when left click
            Vector3 pos = FirePos.transform.position;
            if (direction == Direction.right) // shoot bullet to the right
            {
                GameObject spawnedBullet = Instantiate(bullet, new Vector3(pos.x, pos.y, pos.z), Quaternion.identity);
                spawnedBullet.GetComponent<Rigidbody>().velocity = Vector3.right * bulletSratingSpeed;


            }
            else // shoot bullet to the left
            {
                GameObject spawnedBullet = Instantiate(bullet, new Vector3(pos.x, pos.y, pos.z), Quaternion.identity);
                spawnedBullet.GetComponent<Rigidbody>().velocity = Vector3.left * bulletSratingSpeed;
            }
        }
    }

}