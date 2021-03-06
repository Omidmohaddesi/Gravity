﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonfirController : MonoBehaviour {

    public GameObject createdFire;
    public float emittingFireCD = 2.0f;
    private float distance;
    private bool isInCD = false;
    private Vector3 pos;

	void Start () {
        
	}

    private void Update()
    {
        distance = Vector3.Distance(this.transform.position, GameController.instance.playerControl.transform.position);
        if (distance > 15)
        {
            this.GetComponent<AudioSource>().volume = 0;
        }
        else
        {
            this.GetComponent<AudioSource>().volume = 1 - (distance / 15);

        }
    }

    private void OnTriggerEnter(Collider coll)
    {
        if (coll.gameObject.tag == "Player")
        {
            if (!isInCD)
            {
                isInCD = true;
                pos = transform.position;
                GameObject spawnedFire1 = Instantiate(createdFire, new Vector3(pos.x + 0.3f, pos.y, pos.z), Quaternion.identity);
                spawnedFire1.GetComponent<Rigidbody>().velocity = Vector3.right * 0.7f;
                GameObject spawnedFire2 = Instantiate(createdFire, new Vector3(pos.x - 0.3f, pos.y, pos.z), Quaternion.identity);
                spawnedFire2.GetComponent<Rigidbody>().velocity = Vector3.right * -0.7f;
                GameObject spawnedFire3 = Instantiate(createdFire, new Vector3(pos.x + 0.0f, pos.y, pos.z), Quaternion.identity);
                spawnedFire3.GetComponent<Rigidbody>().velocity = Vector3.right * 0.0f;
                StartCoroutine(EmittingFireCD());
            }
           
        }
    }


    IEnumerator EmittingFireCD()
    {
        yield return (new WaitForSeconds(emittingFireCD));
        isInCD = false;
    }
}
