using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour {

    public float speed = 0.5f;
    public float jumpForce = 3.0f;

    private float h;
    private bool isGrounded = true;
    private Rigidbody rigidBody;
    private Collider collider;
    float disToGround;
    public GameObject bullet;
    public GameObject fireBullet;
    public Transform CharacterSprite;
    public int bulletForce = 100;

    public Animator Character_animator;

    [SerializeField] Gravity gravity;

    enum Direction { left, right};
    Direction direction = Direction.right;

    enum Bullet { normal, fire };
    Bullet bulletType = Bullet.normal;



    // Use this for initialization
    void Start () {
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



    void Update () {
        transform.position += Vector3.right * (Input.GetAxis("Horizontal")) * speed;
        if (Input.GetKeyDown(KeyCode.A))
        {
            direction = Direction.left;
            if (gravity.gravityState==Gravity.gravity.normal) // updating sprite direction *Frank
            {
                CharacterSprite.localScale = new Vector3(1, CharacterSprite.localScale.y, CharacterSprite.localScale.z); 
            }
            else
            {
                CharacterSprite.localScale = new Vector3(-1, CharacterSprite.localScale.y, CharacterSprite.localScale.z);
            }

        }
        if (Input.GetKeyDown(KeyCode.D))
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

        if(Input.GetMouseButtonDown(0))
        {
            if (bulletType == Bullet.normal)
            {
                //spawn bullet when left click
                Vector3 pos = transform.position;
                if (direction == Direction.right) // shoot bullet to the right
                {
                    GameObject spawnedBullet = Instantiate(bullet, new Vector3(pos.x + 0.5f, pos.y, pos.z), Quaternion.identity);
                    //apply force to bullet
                    spawnedBullet.GetComponent<Rigidbody>().AddForce(Vector3.right * bulletForce);                   
                }
                else // shoot bullet to the left
                {
                    GameObject spawnedBullet = Instantiate(bullet, new Vector3(pos.x - 0.5f, pos.y, pos.z), Quaternion.identity);
                    //apply force to bullet
                    spawnedBullet.GetComponent<Rigidbody>().AddForce(Vector3.left * bulletForce);
                }
            }
            else
            {
                //spawn bullet when left click
                Vector3 pos = transform.position;
                if (direction == Direction.right) // shoot bullet to the right
                {
                    GameObject spawnedBullet = Instantiate(fireBullet, new Vector3(pos.x + 0.5f, pos.y, pos.z), Quaternion.identity);
                    //apply force to bullet
                    spawnedBullet.GetComponent<Rigidbody>().AddForce(Vector3.right * bulletForce);
                }
                else // shoot bullet to the left
                {
                    GameObject spawnedBullet = Instantiate(fireBullet, new Vector3(pos.x - 0.5f, pos.y, pos.z), Quaternion.identity);
                    //apply force to bullet
                    spawnedBullet.GetComponent<Rigidbody>().AddForce(Vector3.left * bulletForce);
                }
            }
        }
    }

    void BulletToFire()
    {
        bulletType = Bullet.fire;
    }

   
}
