using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JadeTrigger : MonoBehaviour {



    public GameObject SE;
    public GameObject[] invisiblePlatform;
	void Start () {
		
	}

    //Trigger Activate
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Jade Triggered!");
        if (other.gameObject.tag == "JadeCube")
        {
            Debug.Log("SE activated!");
            SE.SetActive(true);

            foreach(GameObject invP in invisiblePlatform)
            {
                invP.SetActive(true);
            }
        }
    }

    //Trigger Deactivate
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "JadeCube")
        {
            SE.SetActive(false);
            foreach (GameObject invP in invisiblePlatform)
            {
                invP.SetActive(false);
            }
        }
    }

}
