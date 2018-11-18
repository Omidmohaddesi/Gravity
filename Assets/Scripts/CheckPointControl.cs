using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointControl : MonoBehaviour {

    public GameObject SpecialEffect;
    public bool isCheckpointActivated = false;


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            SaveLoadManager.spawnLocation = this.transform.position;
            if (isCheckpointActivated == false)
            {
                if (SpecialEffect != null)
                {
                    SpecialEffect.SetActive(true);
                }
               
                isCheckpointActivated = true;
            }
        }
      
    }


}
