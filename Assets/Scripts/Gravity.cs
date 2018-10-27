using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gravity : MonoBehaviour {

    public enum gravity { normal,reversed};
    public gravity gravityState;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (gravityState == gravity.reversed)
        {
            GetComponent<Rigidbody>().AddForce(Vector3.up * 9.8f * GetComponent<Rigidbody>().mass);
        }
        
	}
}
