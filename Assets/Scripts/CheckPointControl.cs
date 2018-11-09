using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointControl : MonoBehaviour {

    public GameObject SpecialEffect;
    private bool isCheckpointActivated = false;


    private void OnTriggerEnter(Collider other)
    {
        SaveLoadManager.spawnLocation = this.transform.position;
        if (isCheckpointActivated == false)
        {
            SpecialEffect.SetActive(true);
            isCheckpointActivated = true;
        }       
    }


}
