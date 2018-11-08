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

    [HideInInspector]
    public bool dialogue_ended;

    private Queue<string> sentences;

    // Use this for initialization
    void Start()
    {
        canvas.gameObject.SetActive(true);
        sentences = new Queue<string>();
    } 

    public void StartDialogue(Dialogue dialogue)
    {
        dialogue_ended = false;
        Startgame.enabled = false;
        sentences.Clear();
        
        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();       
    }

    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
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
        animator.SetBool("dialogue_ended", true);
    }

}
