using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    private Queue<string> sentences;

    [SerializeField] private TextMeshProUGUI _textComponent;
    [SerializeField] private TextMeshProUGUI _letterTextComponent;
    private Animator _animator;

    private PlayerInput _playerInput;
    private bool _dialogueIsOpen = false;
    private bool _falaDoPlayer;
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
    public void StartDialogue(Dialogue dialogue, bool falaDoPlayer) 
    {
        sentences.Clear();
        _dialogueIsOpen = true;
        _falaDoPlayer = falaDoPlayer;

        if (falaDoPlayer)
            _animator.SetBool("Open", true);
        else
            PaperAnimControl.OnPaperClicked?.Invoke();

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
        _letterTextComponent.text = st;


    }
    private void EndDialogue()
    {
        _dialogueIsOpen = false;
        if(_falaDoPlayer)
            _animator.SetBool("Open", false);
        else
            PaperAnimControl.OnPaperClicked?.Invoke();
    }



}
