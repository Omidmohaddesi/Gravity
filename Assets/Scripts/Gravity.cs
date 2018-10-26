using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gravity : MonoBehaviour {

    public bool isGravityReversed = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (isGravityReversed == true)
        {
            GetComponent<Rigidbody>().AddForce(Vector3.up * 9.8f * 2 * GetComponent<Rigidbody>().mass);
        }
        
	}
}
