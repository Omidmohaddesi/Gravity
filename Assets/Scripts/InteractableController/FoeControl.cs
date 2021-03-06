﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoeControl : MonoBehaviour {

    public float speed = 0.1f;
    public float HP = 100.0f;

    private int patrolDirection = 1; // 1 face right, -1 face left, 0 keep current direction 
    private float disToBound;
    private float actualMoveDir = 1.0f;


    void Start () {
        disToBound = GetComponent<Collider>().bounds.extents.x;
    }

    public int CollisionCheck()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.right, out hit, disToBound + 0.1f))
        {
            if (hit.transform.gameObject.tag != "Player")
            {
                return -1;
            }
            else return 0;
        }
        else if (Physics.Raycast(transform.position, Vector3.left, out hit, disToBound + 0.1f))
        {
            if (hit.transform.gameObject.tag != "Player")
            {
                return 1;
            }
            else return 0;
        }
        else return 0;
    }
    /*
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
  
    }*/

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
        
        if (CollisionCheck() != 0)
        {
            actualMoveDir = CollisionCheck();
        }
        transform.position += Vector3.right * actualMoveDir * Time.deltaTime;
    }
}
