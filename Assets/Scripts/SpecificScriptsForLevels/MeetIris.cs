using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeetIris : MonoBehaviour {

    public GameObject SpiritIris;
    public PlayerControl playerControl;
    private bool hasBeenTriggered = false;


    private void OnTriggerEnter(Collider other)
    {
        if((other.gameObject.tag == "Player")&&(hasBeenTriggered == false))
        {
            hasBeenTriggered = true;
            SpiritIris.SetActive(true);
            SpiritIris.transform.position = playerControl.gameObject.transform.position + Vector3.right * 2.5f + Vector3.up * 1f;
            Debug.Log("Iris!!!");

            playerControl.gravitonAreaUnlocked = true;
        }
    }
}
