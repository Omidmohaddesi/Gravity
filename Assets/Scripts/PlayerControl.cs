using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerControl : MonoBehaviour
{

    #region Character Parameters
    [Header("Character Parameters")]
    public float speed = 0.5f;
    public float jumpForce = 3.0f;
    #endregion

    #region References
    [Header("References")]
    public GameObject gravitonAreaPrefab;
    public GameObject SEJump;
    private GameObject gravitonArea;
    public Transform CharacterSprite;

    public Animator Character_animator;
    public ParticleSystem seJumpPS;
    public Rigidbody rigidBody;
    #endregion

    #region Game Conrol Varaibles
    [Header("Game Conrol Varaibles")]
    private bool isGravityAreaActive = false;

    private float disToGround;
    private bool isGrounded = true;
    private bool isChanting = false;
    #endregion

    #region old varaible initilization trash can
    //public float bulletSratingSpeed = 5.0f;
    //public GameObject bullet;
    //public float bulletForce = 100.0f;
    //public GameObject FirePos;
    #endregion


    // Use this for initialization
    void Start()
    {
        disToGround = GetComponent<Collider>().bounds.extents.y;
        //instantiate gravitonArea for pernament use in this level.
        gravitonArea = Instantiate(gravitonAreaPrefab, this.transform.position, Quaternion.identity);
        gravitonArea.SetActive(false);

        //start intro dialogue
    }

    void Update()
    {
        Control();
        Animation();
        Abilities();


        Debug.DrawRay((transform.position + Vector3.right * 0.3f), Vector3.down * (disToGround + 0.1f), Color.red);
        Debug.DrawRay((transform.position - Vector3.right * 0.3f), Vector3.down * (disToGround + 0.1f), Color.red);
        //!!!!This is a debugging function!!!
        //Remove it when officiallt build!
        if (Input.GetKeyDown(KeyCode.R))
        {
            GameController.instance.ReloadScene();
        }
    }

    public bool GroundCheck()
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position + Vector3.right * 0.3f, -Vector3.up, out hit, disToGround + 0.1f) 
            || Physics.Raycast(transform.position - Vector3.right * 0.3f, -Vector3.up, out hit, disToGround + 0.1f)) 
        {
            if (hit.transform.gameObject.GetComponent<BoxCollider>().isTrigger == false)
            {
                return true;
            }
            else return false;
        }
        else return false;
    }

    public bool ChantCheck()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, -Vector3.up, out hit, disToGround + 0.1f))
        {
            if ((hit.transform.gameObject.GetComponent<BoxCollider>().isTrigger == false) && (hit.transform.gameObject.isStatic == true))
            {
                return true;
            }
            else return false;
        }
        else return false;
    }

    public void Animation()
    {
        if ((Input.GetAxis("Horizontal") > -0.1f) && Input.GetAxis("Horizontal") < 0.1f)
        {
            Character_animator.SetBool("Character_walking", false);
        }
        else Character_animator.SetBool("Character_walking", true);
    }

    public void Control()
    {
        //move character
        if (isChanting == false)
        {
            transform.position += Vector3.right * (Input.GetAxis("Horizontal")) * speed; 
        }

        //turn character
        if ((Input.GetAxis("Horizontal") < 0f) && (isChanting == false))
        {
            CharacterSprite.localScale = new Vector3(1, CharacterSprite.localScale.y, CharacterSprite.localScale.z);
        }
        if ((Input.GetAxis("Horizontal") > 0f) && (isChanting == false))
        {
            CharacterSprite.localScale = new Vector3(-1, CharacterSprite.localScale.y, CharacterSprite.localScale.z);
        }

        //Jump
        if (Input.GetKeyDown(KeyCode.Space) && (isChanting == false))
        {
            isGrounded = GroundCheck();

            if (isGrounded)   //If player is in the air, cannot jump
            {
                isGrounded = false;   //Player's jumping now! cannot jump again
                seJumpPS.Play();
                rigidBody.velocity = Vector3.zero;
                rigidBody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                
            }
        }
    }

    public void Abilities()
    {
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
                if (ChantCheck() && Input.GetAxis("Horizontal") == 0f)
                {
                    StartCoroutine("ChantingForGravityArea");

                }
                else return;
            }
        }
    }

    IEnumerator ChantingForGravityArea()
    {
        isChanting = true;
        Character_animator.SetBool("Character_walking", false);
        Character_animator.SetBool("Character_chanting", true);
        yield return (new WaitForSeconds(1.2f));
        isGravityAreaActive = true;
        gravitonArea.SetActive(true);
        gravitonArea.transform.position = this.transform.position;
        yield return (new WaitForSeconds(0.19f));
        isChanting = false;
        Character_animator.SetBool("Character_chanting", false);       
    }

    #region old function trash can
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
    #endregion
}