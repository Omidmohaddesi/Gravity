using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeetGForce : MonoBehaviour {


    public GameObject seGForceReady;
    public PlayerControl playerControl;
    private bool hasBeenTriggered = false;


    private void OnTriggerEnter(Collider other)
    {
        if((other.gameObject.tag == "Player")&&(hasBeenTriggered == false))
        {
            hasBeenTriggered = true;
            playerControl.gravitonForceUnlocked = true;
            seGForceReady.SetActive(true);
        }
    }
}
