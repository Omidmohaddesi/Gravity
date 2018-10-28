using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour {

    public float lifeTime = 5.0f;
    

    private void Start()
    {
        //lifetime timer
        StartCoroutine(startLifeTime());
    }
    
    IEnumerator startLifeTime ()
    {
        yield return (new WaitForSeconds(lifeTime));
        if (this.gameObject != null)
        {
            Destroy(gameObject);
        }
    }
}
