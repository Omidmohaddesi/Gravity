using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundartWall : MonoBehaviour {

    // If the player collides this, she dies.
    private void OnTriggerEnter(Collider coll)
    {
        if (coll.gameObject.tag == "Player")
        {
            GameController.instance.ReloadScene();
        }
    }
}
