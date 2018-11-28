using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointControl : MonoBehaviour {

    public GameObject SpecialEffect;
    public bool isCheckpointActivated = false;
    public GameObject SE_Activate;
    private GameObject se_active;

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
                se_active = Instantiate(SE_Activate, transform.position, Quaternion.identity);
                StartCoroutine("SE_ActiveLifetime");
            }
        }
    }

    IEnumerator SE_ActiveLifetime()
    {
        yield return (new WaitForSeconds(2.0f));
        Destroy(se_active);
    }
}
