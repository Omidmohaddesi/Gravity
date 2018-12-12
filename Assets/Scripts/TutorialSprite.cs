using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialSprite : MonoBehaviour {

    public GameObject tutorialText;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            tutorialText.GetComponent<ParticleSystem>().Play();
        }
    }

    

}
