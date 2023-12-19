using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    private Queue<string> sentences;

    [SerializeField] private TextMeshProUGUI _textComponent;
    private Animator _animator;

    private PlayerInput _playerInput;
    private bool _dialogueIsOpen = false;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _playerInput = new PlayerInput();
    }

    private void OnEnable()
    {
        _playerInput.Enable();
        _playerInput.Dialogue.Interact.performed += ContinueDialogue;
    }


    private void OnDisable()
    {
        _playerInput.Disable();
        _playerInput.Dialogue.Interact.performed -= ContinueDialogue;
    }


    private void ContinueDialogue(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        DisplayNextMessage();
    }

    private void Start()
    {
        sentences = new Queue<string>();
    }
    public void StartDialogue(Dialogue dialogue) 
    {
        sentences.Clear();
        _dialogueIsOpen = true;
        _animator.SetBool("Open", true);

        foreach(var st in dialogue.Sentences)
        {
            sentences.Enqueue(st);
        }

        DisplayNextMessage();

    }

    public void DisplayNextMessage()
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
        _dialogueIsOpen = false;
        _animator.SetBool("Open", false);
    }



}
