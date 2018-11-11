using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public Dialogue StartingDialogue;
    public Dialogue DeathDialogue;

    public void TriggerDialogue()
    {
        if (SaveLoadManager.StartDialogueFinished==false)
        {
            FindObjectOfType<DialogueManager>().StartDialogue(StartingDialogue);
        }
    }

    public void TriggerDeathDialogue()
    {
        FindObjectOfType<DialogueManager>().StartDialogue(DeathDialogue);
    }


}
