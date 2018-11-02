using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerControl : MonoBehaviour
{
    
    //basic setting
    public float speed = 0.5f;
    public float jumpForce = 3.0f;
    //public float bulletSratingSpeed = 5.0f;
    //public GameObject bullet;
    //public float bulletForce = 100.0f;
    public GameObject FirePos;

    //graviton variants
    [SerializeField] public GameObject gravitonAreaPrefab;
    private GameObject gravitonArea;
    private bool isGravityAreaActive = false;

    public Animator Character_animator;
    public Transform CharacterSprite;


    public GameObject SEJump;


    //private variants
    private float h;
    private bool isGrounded = true;
    private Rigidbody rigidBody;
    private Collider collider;
    private float disToGround;
    private ParticleSystem seJumpPS;
    private bool isChanting = false;

    [SerializeField] Gravity gravity;
    enum Direction { left, right };
    Direction direction = Direction.right;


    // Use this for initialization
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        collider = GetComponent<Collider>();
        disToGround = collider.bounds.extents.y;
        seJumpPS = SEJump.GetComponent<ParticleSystem>();

        //instantiate gravitonArea for pernament use in this level.
        gravitonArea = Instantiate(gravitonAreaPrefab, this.transform.position, Quaternion.identity);
        gravitonArea.SetActive(false);

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

        if (isChanting == false)
        {
            transform.position += Vector3.right * (Input.GetAxis("Horizontal")) * speed;
        }
        
        if ((Input.GetAxis("Horizontal") > -0.1f) && Input.GetAxis("Horizontal") < 0.1f)
        {
            Character_animator.SetBool("Character_walking", false);
        }
        else Character_animator.SetBool("Character_walking", true);

        if (Input.GetKeyDown(KeyCode.LeftArrow) && (isChanting == false))
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
        if (Input.GetKeyDown(KeyCode.RightArrow) && (isChanting == false))
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
        //Reverse Gravity: discarded. Now we use Graviton Area
        /*
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
        */

        //Jump
        if (Input.GetKeyDown(KeyCode.Space) && (isChanting == false))
        {

            
            isGrounded = GroundCheck();

            if (isGrounded)   //If player is in the air, cannot jump
            {
                isGrounded = false;   //Player's jumping now! cannot jump again
                seJumpPS.Play();

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

        //Gravity Energy Ball
        /*
        if (Input.GetKeyDown(KeyCode.X))
        {
            
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
        */

        //Graviton Area
        if (Input.GetKeyDown(KeyCode.X) && (isChanting == false))
        {
            if (isGravityAreaActive == true)
            {
                isGravityAreaActive = false;
                gravitonArea.SetActive(false);
            }
            else if (isGravityAreaActive == false)
            {
                if (GroundCheck())
                {
                    StartCoroutine("ChantingForGravityArea");

                }
                else return;
            }
        }

        //!!!!This is a debugging function!!!
        //Remove it when officiallt build!
        if (Input.GetKeyDown(KeyCode.R))
        {
            Scene scene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(scene.name);
        }
    }


    IEnumerator ChantingForGravityArea()
    {
        isChanting = true;
        Character_animator.SetBool("Character_walking", false);
        Character_animator.SetBool("Character_chanting", true);
        yield return (new WaitForSeconds(2.0f));
        isGravityAreaActive = true;
        gravitonArea.SetActive(true);
        gravitonArea.transform.position = this.transform.position;
        yield return (new WaitForSeconds(1.0f));
        isChanting = false;
        Character_animator.SetBool("Character_walking", false);
        Character_animator.SetBool("Character_chanting", false);
        
    }
}