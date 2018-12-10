using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IrisLeaves : MonoBehaviour {

    public PlayerControl playerCtrol;
    public GameObject Iris;

    public GameObject audio1;
    public GameObject audio2;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {

            Iris.SetActive(false);
            playerCtrol.gravitonAreaUnlocked = false;

            audio1.SetActive(false);
            audio2.SetActive(true);
        }
    }


}
