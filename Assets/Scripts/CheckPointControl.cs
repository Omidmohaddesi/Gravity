using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointControl : MonoBehaviour {

    public GameObject SpecialEffect;
    private bool isCheckpointActivated = false;

    private void OnTriggerEnter(Collider other)
    {
        if (isCheckpointActivated == false)
        {
            SpecialEffect.SetActive(true);
            isCheckpointActivated = true;
        }
        
        
    }


}
