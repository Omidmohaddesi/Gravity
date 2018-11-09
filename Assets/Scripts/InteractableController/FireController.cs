using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireController : MonoBehaviour {

    public bool isGravityReversed = false;
    public float lifeTime = 15.0f;
    public float speed = 2.0f;

    private Rigidbody rb;
 


    private void Start()
    {
        //lifetime timer
        StartCoroutine(startLifeTime());
        rb = GetComponent<Rigidbody>();
    
    }

    private void Update()
    {
        if (isGravityReversed)
        {
            rb.AddForce(Vector3.up * speed * (-1));
        }
        else
        {
            rb.AddForce(Vector3.up * speed * (1));
        }
    }

    private void OnTriggerEnter(Collider coll)
    {
        //Hit by GEB
        if (coll.gameObject.tag == "GEB")
        {
            Debug.Log("Fire hits GEB!!!!");
            if (isGravityReversed == false)
            {
                isGravityReversed = true;
            }
            else
            {
                isGravityReversed = false;
            }
        }

    }

    IEnumerator startLifeTime()
    {
        yield return (new WaitForSeconds(lifeTime));
        if (this.gameObject != null)
        {
            Destroy(gameObject);
        }
    }

 
}
