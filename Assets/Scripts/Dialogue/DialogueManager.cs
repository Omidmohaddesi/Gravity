using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public Text dialogueText;
    public Text Startgame;
    public Animator animator;
    public Canvas canvas;
    public PlayerControl playerControl;
    public GameController gameController;

    public Image Character_1;
    public Image Character_2;

    [HideInInspector]
    public bool dialogue_ended;
    private bool isConversation;
    private bool isDeathConversation;

    private Queue<string> sentences;

    // Use this for initialization
    void Start()
    {
        sentences = new Queue<string>();

        if (!SaveLoadManager.StartDialogueFinished) {
            canvas.gameObject.SetActive(true);
        }
    } 

    public void StartDialogue(Dialogue dialogue)
    {
        canvas.gameObject.SetActive(true);
        animator.SetBool("dialogue_ended", false);
        dialogue_ended = false;
        Startgame.enabled = false;
        dialogueText.enabled = true;
        playerControl.enabled = false;
        sentences.Clear();

        isConversation = false;

        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();       
    }

    public void StartConversation(Conversation conversation)
    {
        canvas.gameObject.SetActive(true);
        animator.SetBool("dialogue_ended", false);
        dialogue_ended = false;
        Startgame.enabled = false;
        dialogueText.enabled = true;
        playerControl.enabled = false;
        sentences.Clear();
        isConversation = true;

        if(conversation.name == "DeathConversation")
        {
            isDeathConversation = true;
        }

        Character_1.sprite = conversation.portrait_1_talksfirst;
        Character_2.sprite = conversation.portrait_2;

        foreach (string sentence in conversation.sentences)
        {
            sentences.Enqueue(sentence);
        }
        
       
        DisplayNextSentence();
        Character_1.enabled = true;
    }

    public void DisplayNextSentence()
    {
        if (!isConversation)
        {
            if (sentences.Count == 0)
            {
                EndDialogue();
                return;
            }
        }

        else
        {
            if (sentences.Count == 0)
            {
                Character_2.enabled = false;
                Character_1.enabled = false;
                EndDialogue();
                return;
            }

            if (Character_1.enabled == true)
            {
                Character_2.enabled = true;
                Character_1.enabled = false;
            }

            else 
            {
                Character_2.enabled = false;
                Character_1.enabled = true;
            }

        }
        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }


    IEnumerator TypeSentence(string sentence)
    {
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return null;
        }
    }

    void EndDialogue()
    {
        dialogue_ended = true;
        dialogueText.enabled = false;
        playerControl.enabled = true;
        animator.SetBool("dialogue_ended", true);
        if (SaveLoadManager.StartDialogueFinished == false)
        {
            SaveLoadManager.StartDialogueFinished = true;
        }

        if (isDeathConversation)
        {
            gameController.readytoReload = true;
        }
    }

}
