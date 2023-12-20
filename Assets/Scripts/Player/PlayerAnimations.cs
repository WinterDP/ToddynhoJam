using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{

    private Animator _animator;
    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void Start()
    {
        _animator.Play("IdleBase");
    }
    public void PlayAttack()
    {
        _animator.Play("Attack");
    }

    public void PlayAgachando()
    {
        _animator.Play("Agachando");
    }

    public void PlayGuardarArma()
    {
        _animator.Play("GuardarArma");
    }

    public void PlayIdleArmado()
    {
        _animator.Play("IdleArmado");
    }

    public void PlayLevantando()
    {
        _animator.Play("AgachadoComArma");
    }

    public void PlayAgachadoComArma()
    {
        _animator.Play("AgachadoComArma");
    }

    public void PlayAgachadoBase()
    {
        _animator.Play("AgachadoBase");
    }

    public void PlayAgachadoAtirando()
    {
        _animator.Play("AgachadoAtira");
    }

    public void PlayPlayerBaseComArma()
    {
        _animator.Play("PlayerBaseComArma");
    }

    public void PlayDesarmado()
    {
        _animator.Play("Desarmado");
    }

    public void PlayInteract()
    {
        _animator.Play("Interact");
    }

    public void PlayRunning()
    {
        _animator.Play("Running");
    }

    public void PlayRunningComArma()
    {
        _animator.Play("RunningComArma");
    }

    public void Reload()
    {
        _animator.Play("Reload");
    }

    public void Shoot()
    {
        _animator.Play("Shoot");
    }

}
