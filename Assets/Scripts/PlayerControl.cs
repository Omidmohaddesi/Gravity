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
    public float pushForce = 1.0f;
    public bool gravitonAreaUnlocked = true;    //whether she can use "X - GravitonArea" ability.
    public bool gravitonForceUnlocked = false;  //whetehr she can use "Z - GravitonForce" ability.
    public float gForceRadius = 3.0f;
    public float gForceForce = 20.0f;
    public float gForceCD = 5.0f;
    #endregion

    #region References
    [Header("References")]
    public GameObject spiritIris;
    public GameObject spiritIrisPos;
    private Vector3 spiritIrisDestinationPos;

    public GameObject gravitonAreaPrefab;
    public GameObject SEJump;
    private GameObject gravitonArea;
    public Transform CharacterSprite;

    public Animator Character_animator;
    public ParticleSystem seJumpPS;
    public ParticleSystem seGravitonForcePS;
    public GameObject seGForceReady;
    public Rigidbody rigidBody;
    #endregion

    #region Game Conrol Varaibles
    [Header("Game Conrol Varaibles")]
    private bool isGravityAreaActive = false;

    private float disToGround;
    private float disToSide;

    private bool isGrounded = true;
    private bool isChantingGravityArea = false;
    private bool isChantingGravityForce = false;
    private bool canChantGravityForce = true;
    #endregion



    // Use this for initialization
    void Start()
    {
        disToGround = GetComponent<Collider>().bounds.extents.y;
        disToSide = GetComponent<Collider>().bounds.extents.x;
        if (gravitonForceUnlocked)
        {
            seGForceReady.SetActive(true);
        }

        //instantiate gravitonArea for pernament use in this level.
        gravitonArea = Instantiate(gravitonAreaPrefab, this.transform.position, Quaternion.identity);
        gravitonArea.SetActive(false);

    }

    void Update()
    {
        Control();
        Animation();
        Abilities();
        SpiritIris();

        Debug.DrawRay((transform.position + Vector3.right * 0.3f), Vector3.down * (disToGround + 0.1f), Color.red);
        Debug.DrawRay((transform.position - Vector3.right * 0.3f), Vector3.down * (disToGround + 0.1f), Color.red);
        Debug.DrawRay((transform.position + Vector3.up * 0.5f), Vector3.left * (disToSide + 0.07f), Color.blue);
        Debug.DrawRay((transform.position + Vector3.down * 0.3f), Vector3.left * (disToSide + 0.07f), Color.blue);
        Debug.DrawRay((transform.position), Vector3.down * 0.7f, Color.blue);
        //!!!!This is a debugging function!!!
        //Remove it when officiallt build!
        if (Input.GetKeyDown(KeyCode.R))
        {
            GameController.instance.ReloadScene();
        }
    }

    public bool SideCheckLeft()
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position + Vector3.up * 0.5f, Vector3.left, out hit, disToSide + 0.07f)
         || Physics.Raycast(transform.position + Vector3.down * 0.3f, Vector3.left, out hit, disToSide + 0.07f))
        {
            if ((hit.transform.gameObject.GetComponent<Collider>().isTrigger == false))
            {
                if (hit.transform.gameObject.isStatic == false)
                {
                    Rigidbody hitRB = hit.transform.gameObject.GetComponent<Rigidbody>();
                    if (hitRB != null)
                    {
                        Vector3 v = hitRB.velocity;
                        v.x = 0f;
                        hitRB.velocity = v;
                        hitRB.AddForce(Vector3.left * pushForce, ForceMode.Impulse);
                    }
                }
                return true;
            }
            else return false;
        }
        else return false;
    }

    public bool SideCheckRight()
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position + Vector3.up * 0.5f, Vector3.right, out hit, disToSide + 0.07f)
         || Physics.Raycast(transform.position + Vector3.down * 0.3f, Vector3.right, out hit, disToSide + 0.07f))
        {
            if ((hit.transform.gameObject.GetComponent<Collider>().isTrigger == false))
            {
                if (hit.transform.gameObject.isStatic == false)
                {
                    Rigidbody hitRB = hit.transform.gameObject.GetComponent<Rigidbody>();
                    if (hitRB != null)
                    {
                        Vector3 v = hitRB.velocity;
                        v.x = 0f;
                        hitRB.velocity = v;
                        hitRB.AddForce(Vector3.right * pushForce, ForceMode.Impulse);
                    }
                }
                return true;
            }
            else return false;
        }
        else return false;
    }

    public bool GroundCheck()
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position + Vector3.right * 0.3f, -Vector3.up, out hit, disToGround + 0.1f)
            || Physics.Raycast(transform.position - Vector3.right * 0.3f, -Vector3.up, out hit, disToGround + 0.1f)
            || Physics.Raycast(transform.position + Vector3.right * 0.15f, -Vector3.up, out hit, disToGround + 0.1f)
            || Physics.Raycast(transform.position - Vector3.right * 0.15f, -Vector3.up, out hit, disToGround + 0.1f)
            || Physics.Raycast(transform.position, -Vector3.up, out hit, disToGround + 0.1f))
        {
            if (hit.transform.gameObject.GetComponent<Collider>().isTrigger == false)
            {
                return true;
            }
            else return false;
        }
        else return false;
    }

    public bool ChantCheck() //BIG Problem: Will GO WRONG AFTER BUILDING!!!!!
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, -Vector3.up, out hit, disToGround+0.1f))
        {
            if ((hit.transform.gameObject.GetComponent<Collider>().isTrigger == false) && (hit.transform.gameObject.layer == 8))
            {
                return true;
            }
            else return false;
        }
        else return false;
    }

    public void Animation()
    {
        if (((Input.GetAxis("Horizontal") > 0.1f) || (Input.GetAxis("Horizontal") < -0.1f)) && (isChantingGravityArea == false) && (isChantingGravityForce == false))
        {
            Character_animator.SetBool("Character_walking", true);
        }
        else Character_animator.SetBool("Character_walking", false);
    }

    public void Control()
    {
        //move character
        if ((isChantingGravityArea == false) && (isChantingGravityForce == false))
        {
            if ((Input.GetAxis("Horizontal")) > 0f)
            {
                if (SideCheckRight() == false)
                {
                    //rigidBody.MovePosition((Vector3.right * (Input.GetAxis("Horizontal")) * speed) + rigidBody.position);
                    transform.position += Vector3.right * (Input.GetAxis("Horizontal")) * speed;
                }
            }
            else if ((Input.GetAxis("Horizontal")) < 0f)
            {
                if (SideCheckLeft() == false)
                {
                    //rigidBody.MovePosition((Vector3.right * (Input.GetAxis("Horizontal")) * speed) + rigidBody.position);
                    transform.position += Vector3.right * (Input.GetAxis("Horizontal")) * speed;
                }
            }

        }

        //turn character
        if ((Input.GetAxis("Horizontal") < 0f) && (isChantingGravityArea == false))
        {
            CharacterSprite.localScale = new Vector3(1, CharacterSprite.localScale.y, CharacterSprite.localScale.z);
        }
        if ((Input.GetAxis("Horizontal") > 0f) && (isChantingGravityArea == false))
        {
            CharacterSprite.localScale = new Vector3(-1, CharacterSprite.localScale.y, CharacterSprite.localScale.z);
        }

        //Jump
        if (Input.GetKeyDown(KeyCode.Space) && (isChantingGravityArea == false))
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

    public void SpiritIris()
    {
        if (spiritIris != null)
        {
            if (isGravityAreaActive == false) spiritIrisDestinationPos = spiritIrisPos.transform.position;
            spiritIris.transform.position = Vector3.Lerp(spiritIris.transform.position, spiritIrisDestinationPos, Time.deltaTime);
        }


    }

    public void Abilities()
    {
        
        ////debug!!!!
        //if (Input.GetButtonDown("X"))
        //{
        //    transform.position = transform.position + Vector3.up * 3.0f;
        //}
        
        //Graviton Area
        if ((Input.GetButtonDown("X")) && (isChantingGravityArea == false) && (isChantingGravityForce == false) && (gravitonAreaUnlocked))
        {
            
            if (isGravityAreaActive == true)
            {
                isGravityAreaActive = false;
                gravitonArea.SetActive(false);
            }
            else if (isGravityAreaActive == false)
            {
                
                if (ChantCheck() && (Input.GetAxis("Horizontal") < 0.1f && Input.GetAxis("Horizontal") > -0.1f))
                {
                    
                    StartCoroutine("ChantingForGravityArea");
             
                }
                else return;
            }
        }

        //Graviton Force
        if ((Input.GetButtonDown("Z")) && (isChantingGravityArea == false) && (isChantingGravityForce == false) && (gravitonForceUnlocked))
        {
            if (canChantGravityForce)
            {
                StartCoroutine("ChantingForGravityForce");
                StartCoroutine("GravityForceCD");
            }
            
        }

    }

    IEnumerator ChantingForGravityArea()
    {
        
        isChantingGravityArea = true;
        Character_animator.SetBool("Character_walking", false);
        Character_animator.SetBool("Character_chanting", true);

        yield return (new WaitForSeconds(1.2f));

        isGravityAreaActive = true;
        gravitonArea.SetActive(true);
        gravitonArea.transform.position = this.transform.position;

        spiritIrisDestinationPos = this.transform.position + Vector3.up * 2.5f;


        yield return (new WaitForSeconds(0.19f));

        isChantingGravityArea = false;
        Character_animator.SetBool("Character_chanting", false);
    }

    IEnumerator ChantingForGravityForce()
    {
        //前摇
        isChantingGravityForce = true;
        
        Character_animator.SetBool("Character_walking", false);
        Character_animator.SetBool("Character_chanting", false);
        Character_animator.SetBool("Character_chantingForce", true);
        rigidBody.useGravity = false;
        rigidBody.velocity = Vector3.zero;
        yield return (new WaitForSeconds(0.65f));

        //原力效果
        seGravitonForcePS.Play();
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, gForceRadius);
        int i = 0;
        while (i < hitColliders.Length)
        {
            if ((hitColliders[i].isTrigger == false))
            {
                if ((hitColliders[i].transform.gameObject.isStatic == false) && (hitColliders[i].gameObject.tag != "Player"))
                {
                    Rigidbody hitRB = hitColliders[i].transform.gameObject.GetComponent<Rigidbody>();
                    if (hitRB != null)
                    {
                        Vector3 v = hitRB.velocity;
                        v = Vector3.zero;
                        hitRB.velocity = v;
                        hitRB.AddForce((hitColliders[i].transform.position - (transform.position)) * gForceForce, ForceMode.Impulse);
                        
                        if (hitRB.mass >= 5)
                        {
                            hitRB.AddForce((hitColliders[i].transform.position - (transform.position)) * gForceForce * 4f, ForceMode.Impulse);
                        }
                    }
                }
                
            }
            i++;
        }

        //后摇
        yield return (new WaitForSeconds(0.55f));
        isChantingGravityForce = false;
        rigidBody.useGravity = true;
        rigidBody.velocity = Vector3.zero;
        yield return (new WaitForSeconds(0.3f));
        Character_animator.SetBool("Character_walking", false);
        Character_animator.SetBool("Character_chanting", false);
        Character_animator.SetBool("Character_chantingForce", false);
    }

    IEnumerator GravityForceCD()
    {
        seGForceReady.SetActive(false);
        canChantGravityForce = false;
        yield return (new WaitForSeconds(gForceCD));
        canChantGravityForce = true;
        seGForceReady.SetActive(true);

    }
    
    }