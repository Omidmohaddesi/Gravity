using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public PlayerControl playerControl;
    public Dialogue dialogue;

    public void TriggerDialogue()
    {
        playerControl.enabled = true;
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
    }

}
