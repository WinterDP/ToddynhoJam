using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    private Queue<string> sentences;

    [SerializeField] private TextMeshProUGUI _textComponent;


    private void Start()
    {
        sentences = new Queue<string>();
    }
    public void StartDialogue(Dialogue dialogue) 
    {
        sentences.Clear();

        foreach(var st in dialogue.Sentences)
        {
            sentences.Enqueue(st);
        }

        DisplayNextMessage();

    }

    private void DisplayNextMessage()
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        string st = sentences.Dequeue();
        _textComponent.text = st;


    }
    private void EndDialogue()
    {
        // close text box animation
    }



}
