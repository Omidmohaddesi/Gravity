using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireController : MonoBehaviour {

    public GameObject player;

    private void OnTriggerEnter(Collider other)
    {
        player.SendMessage("BulletToFire");
    }
}
