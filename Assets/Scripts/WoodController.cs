using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoodController : MonoBehaviour {

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "fire bullet")
            Destroy(gameObject);
    }
}
