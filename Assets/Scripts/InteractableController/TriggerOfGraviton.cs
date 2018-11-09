using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerOfGraviton : MonoBehaviour {


    public GameObject SE;
    public GameObject GravitonArea;

    public bool isActivated = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (isActivated == false)
            {
                SE.gameObject.SetActive(true);
                GravitonArea.gameObject.SetActive(true);
                isActivated = true;
            }
            else if (isActivated == true)
            {
                SE.gameObject.SetActive(false);
                GravitonArea.gameObject.SetActive(false);
                isActivated = false;
            }


        }
    }
}
