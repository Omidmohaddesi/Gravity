using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour {
    private Queue<string> sentences;
    public Text dialogue_text;
    static public bool dialogue_ended = false;

	// Use this for initialization
	void Start () {
        sentences = new Queue<string>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void StartDialogue(Dialogue dialogue)
    {
        dialogue_ended = false;
        sentences.Clear();
        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DispplayNextSentence();
    }

    public void DispplayNextSentence()
    {
        if(sentences.Count==0)
        {
            EndDialogue();           
            return;
        }

        string sentence = sentences.Dequeue();
        dialogue_text.text = sentence;
    }

    public void EndDialogue()
    {
        dialogue_text.text = "";
        dialogue_ended = true;
    }
}
