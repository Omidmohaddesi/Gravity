using System.Collections;
using System.Collections.Generic;
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
    public ConversationTrigger conversationTrigger;
    public SaveLoadManager saveLoadManager;
    public bool readytoReload;
    public Fade fade;
    public bool EndingScene;

    // Use this for initialization
    void Start () {
        ReadyForNewScene();
        #region reference in code so don't have to reference in inspector for every scene
        saveLoadManager = GameObject.Find("SaveLoadController").GetComponent<SaveLoadManager>();
        fade = GameObject.Find("BlackFade").GetComponent<Fade>();
        #endregion
    }

    // Update is called once per frame
    void Update () {
        
        if (readytoReload == true && fade.fadeOutCompleted)
        { 
           saveLoadManager.ReloadCurrentScene();
        }

        if (readytoReload != true && fade.fadeOutCompleted)
        {
            saveLoadManager.LoadNextScene();
        }
    }

    public void ReloadScene()
    {
        fade.FadeOut();
        readytoReload = true;
        conversationTrigger.TriggerDeathConversation();
        playerControl.enabled = false;
    }

    public void LoadNextScene()
    {
        fade.FadeOut();       
        playerControl.enabled = false;
    }

    public void ReadyForNewScene()
    {
        playerControl.transform.position = SaveLoadManager.spawnLocation;
        if (SaveLoadManager.StartDialogueFinished == true)
        {
            playerControl.enabled = true;
            Camera.main.orthographicSize = 5f;
        }

    }
}
