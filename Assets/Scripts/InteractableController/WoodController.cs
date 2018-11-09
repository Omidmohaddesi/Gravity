using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoodController : MonoBehaviour {

    public bool isGravityReversed = false;
    
    public float ignitingLifetime = 4.0f;
    public GameObject createdFire;
    public float fireSpeed = 1.0f;
    public GameObject fireSE;

    private Rigidbody rb;
    private Vector3 pos;
    private bool isIgnited = false;


    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (isGravityReversed)
        {
            rb.AddForce(Vector3.up * 9.8f * 2 * rb.mass);
        }
    }

    private void OnTriggerStay(Collider coll)
    {
        if (coll.gameObject.tag == "GEB")
        {
            Debug.Log("WOOD hits GEB!!!!");
            if (isGravityReversed == false)
            {
                isGravityReversed = true;
            }
            else
            {
                isGravityReversed = false;
            }

            Destroy(coll.gameObject);
        }

        //Hit by Fire
        if (coll.gameObject.tag == "Fire")
        {
            Debug.Log("Fire hits GEB!!!!");
            Destroy(coll.gameObject);
            if (isIgnited == false)
            {
                isIgnited = true;
                StartCoroutine(OnFire());
                
            }


        }
    }

    IEnumerator OnFire()
    {
        
        fireSE.SetActive(true);
        yield return (new WaitForSeconds(ignitingLifetime));
        pos = transform.position;
        GameObject spawnedFire1 = Instantiate(createdFire, new Vector3(pos.x + 0.3f, pos.y, pos.z), Quaternion.identity);
        spawnedFire1.GetComponent<Rigidbody>().velocity = Vector3.right * 0.2f;
        GameObject spawnedFire2 = Instantiate(createdFire, new Vector3(pos.x - 0.3f, pos.y, pos.z), Quaternion.identity);
        spawnedFire2.GetComponent<Rigidbody>().velocity = Vector3.right * -0.2f;
        GameObject spawnedFire3 = Instantiate(createdFire, new Vector3(pos.x + 0.0f, pos.y, pos.z), Quaternion.identity);
        spawnedFire3.GetComponent<Rigidbody>().velocity = Vector3.right * 0.0f;
        Destroy(gameObject);



    }
}
