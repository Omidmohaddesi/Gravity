using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal_next_scene : MonoBehaviour {
    public SaveLoadManager saveLoadManager;
    public GameController gameController;
    public void Start()
    {
        #region reference in code so don't have to reference in inspector for every scene
        saveLoadManager = GameObject.Find("SaveLoadController").GetComponent<SaveLoadManager>();
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
        #endregion
    }

    public void OnTriggerStay (Collider other)
    {
        SaveLoadManager.spawnLocation = new Vector3(2, 7, 0);
        gameController.LoadNextScene();
    }
}
