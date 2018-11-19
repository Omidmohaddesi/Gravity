using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public Text dialogueText;
    public Text nameText;
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
    bool inConversation;

    private Queue<string> sentences;
    private Queue<Line> lines;

    // Use this for initialization
    void Start()
    {
        sentences = new Queue<string>();
        lines = new Queue<Line>();

        if (!SaveLoadManager.StartDialogueFinished) {
            canvas.gameObject.SetActive(true);
        }
    }

    private void Update()
    {
        if (inConversation && Input.GetKeyDown(KeyCode.Space))
        {
            DisplayNextLine();
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

        DisplayNextLine();       
    }

    public void StartConversation(Conversation conversation)
    {
        inConversation = true;
        canvas.gameObject.SetActive(true);
        //animator.SetBool("dialogue_ended", false);
        dialogue_ended = false;
        Startgame.enabled = false;
        nameText.enabled = true;
        dialogueText.enabled = true;
        playerControl.enabled = false;
        sentences.Clear();
        isConversation = true;

        if (conversation.name == "DeathConversation")
        {
            isDeathConversation = true;
        }

        foreach (Line line in conversation.lines)
        {
            lines.Enqueue(line);
        }
        
       
        DisplayNextLine();
        //Character_1.enabled = true;
    }

    public void DisplayNextLine()
    {
        if (!isConversation)
        {
            if (lines.Count == 0)
            {
                EndDialogue();
                return;
            }
        }

        else
        {
            if (lines.Count == 0)
            {
                Character_2.enabled = false;
                Character_1.enabled = false;
                EndDialogue();
                return;
            }

            //if (Character_1.enabled == true)
            //{
            //    Character_2.enabled = true;
            //    Character_1.enabled = false;
            //}

            //else 
            //{
            //    Character_2.enabled = false;
            //    Character_1.enabled = true;
            //}

        }
        Line line = lines.Dequeue();

        // Display the line
        StopAllCoroutines();
        StartCoroutine(TypeSentence(line.text));

        // Display the name
        nameText.text = line.name;

        // Display the portrait
        if (line.portraitPos == 1)
        {
            Character_1.enabled = true;
            Character_2.enabled = false;
            Character_1.sprite = line.portrait;
        }
        else if (line.portraitPos == 2)
        {
            Character_1.enabled = false;
            Character_2.enabled = true;
            Character_2.sprite = line.portrait;
        }
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
        inConversation = false;
        nameText.enabled = false;
        dialogueText.enabled = false;
        playerControl.enabled = true;
        //animator.SetBool("dialogue_ended", true);
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
