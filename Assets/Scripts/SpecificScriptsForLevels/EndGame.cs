using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGame : MonoBehaviour {

    public bool isFinalDiaglogueTriggered = false;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            isFinalDiaglogueTriggered = true;
        }
    }

}
