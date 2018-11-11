using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConversationTrigger : MonoBehaviour
{
    public Conversation conversation;
    public bool conversation_triggered;

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && !conversation_triggered)
        {
            FindObjectOfType<DialogueManager>().StartConversation(conversation);
            conversation_triggered = true;
        }
    }

    public void TriggerDeathConversation()
    {
        FindObjectOfType<DialogueManager>().StartConversation(conversation);
    }
}
