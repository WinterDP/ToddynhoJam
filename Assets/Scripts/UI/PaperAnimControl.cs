using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaperAnimControl : MonoBehaviour
{
    public static Action OnPaperClicked;

    private bool isClosed = true;
    private Animator animator;
    private void Awake()
    {
        animator = GetComponent<Animator>();
        animator.SetBool("Open", false);
    }

    private void OnEnable()
    {
        OnPaperClicked += PaperClicked;
    }

    private void OnDisable()
    {
        OnPaperClicked -= PaperClicked;
    }

    private void PaperClicked()
    {
        if (isClosed)
            animator.SetBool("Open", true);
        else
            animator.SetBool("Open", false);

        isClosed = !isClosed;

    }
}
