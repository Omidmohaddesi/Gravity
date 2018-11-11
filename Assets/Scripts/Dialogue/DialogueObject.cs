using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueObject : MonoBehaviour
{
    public Dialogue ObjectDialogue;
    public Animator curtain_animation;
    public bool dialogue_triggered;


    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player"&& !dialogue_triggered)
        {
            FindObjectOfType<DialogueManager>().StartDialogue(ObjectDialogue);
            dialogue_triggered = true;
        }
    }
}