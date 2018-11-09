using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dangers : MonoBehaviour {

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            GameController.instance.ReloadScene();
        }
    }
}
