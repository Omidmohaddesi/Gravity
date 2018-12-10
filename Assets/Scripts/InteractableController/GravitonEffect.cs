using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravitonEffect : MonoBehaviour {

    private Rigidbody rb;
    public float gravitonForce = 1.0f;
    public bool ifClearXVelocity = false;

    private void Start()
    {
        rb = this.GetComponent<Rigidbody>();
    }
    private void OnTriggerStay(Collider coll)
    {
        
        if (coll.gameObject.tag == "Graviton")
        {
            if (ifClearXVelocity == true)
            {
                Vector3 velo;
                velo = rb.velocity;
                velo.x = 0;
                rb.velocity = velo;
            }
           
            rb.AddForce(Vector3.up * (gravitonForce + 9.8f) * rb.mass);
        }
    }

}
