using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravitonEffect : MonoBehaviour {

    private Rigidbody rb;
    public float gravitonForce = 2.0f;

    private void Start()
    {
        rb = this.GetComponent<Rigidbody>();
    }
    private void OnTriggerStay(Collider coll)
    {
        
        if (coll.gameObject.tag == "Graviton")
        {
            rb.AddForce(Vector3.up * (gravitonForce + 9.8f) * rb.mass);
        }
    }

}
