using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameController : MonoBehaviour {

    #region     //singleton

    public static GameController instance = null;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Debug.LogWarning("more than one instance");
            return;
        }
        instance = this;
    }
    #endregion
    public PlayerControl playerControl;

    // Use this for initialization
    void Start () {
        playerControl.transform.position = SaveLoadManager.spawnLocation;


        if (SaveLoadManager.StartDialogueFinished == true)
        {
            playerControl.enabled = true;
            Camera.main.orthographicSize = 5f;
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ReloadScene()
    {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }
}
