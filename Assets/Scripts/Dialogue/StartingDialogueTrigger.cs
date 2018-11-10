using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartingDialogueTrigger : MonoBehaviour
{
    public PlayerControl playerControl;
    public Dialogue dialogue;

    void Start()
    {
        Debug.Log(SaveLoadManager.StartDialogueFinished);

    }

    private void Update()
    {
        Debug.Log(SaveLoadManager.StartDialogueFinished );
    }

    public void TriggerDialogue()
    {
        if (SaveLoadManager.StartDialogueFinished==false)
        {
            playerControl.enabled = true;
            FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
            SaveLoadManager.StartDialogueFinished = true;
        }
    }

}
