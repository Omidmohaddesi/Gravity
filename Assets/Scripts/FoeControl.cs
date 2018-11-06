using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoeControl : MonoBehaviour {

    public float speed = 0.1f;
    public float HP = 100.0f;

    private int patrolDirection = 1;

	void Start () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if ((other.isTrigger == false) )
        {
            if (patrolDirection == 1)
            {
                patrolDirection = -1;
            }
            else
            {
                patrolDirection = 1;
            }
        }
  
    }

    private void OnCollisionEnter(Collision coll)
    {
        if (coll.gameObject.isStatic == false)
        {
            float collDamage = 0.0f;
            Rigidbody rb = coll.gameObject.GetComponent<Rigidbody>();
            if (coll.gameObject.tag != "Player")
            {
                collDamage = rb.mass * rb.velocity.y * -1.0f;
                Debug.Log(collDamage);
                if (collDamage >= HP)
                {
                    Destroy(gameObject);
                }
            }
        }
 
    }


    void Update () {
        patrol();
	}

    private void patrol()
    {
        transform.position += Vector3.right * patrolDirection * Time.deltaTime;
    }
}
