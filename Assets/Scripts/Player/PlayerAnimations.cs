using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{

    private Animator _animator;
    private Animator _animatorLegs;
    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _animatorLegs = transform.GetChild(0).transform.GetComponent<Animator>();
    }

    private void Start()
    {
        _animator.Play("IdleBase");
        _animatorLegs.Play("PeIdle");
    }
    public void PlayAttack()
    {
        _animator.Play("Attack");
        _animatorLegs.Play("PeIdle");
    }

    public void PlayAgachando()
    {
        _animator.Play("Agachando");
        _animatorLegs.Play("PeIdleAgachado");
    }

    public void PlayGuardarArma()
    {
        _animator.Play("GuardarArma");
        _animatorLegs.Play("PeIdle");
    }

    public void PlayIdleArmado()
    {
        _animator.Play("IdleArmado");
        _animatorLegs.Play("PeIdle");
    }

    public void PlayLevantando()
    {
        _animator.Play("AgachadoComArma");
        _animatorLegs.Play("PeIdle");
    }

    public void PlayAgachadoComArma()
    {
        _animator.Play("AgachadoComArma");
        _animatorLegs.Play("AndandoAgachado");
    }

    public void PlayAgachadoBase()
    {
        _animator.Play("AgachadoBase");
        _animatorLegs.Play("AndandoAgachado");
    }

    public void PlayAgachadoAtirando()
    {
        _animator.Play("AgachadoAtira");
        _animatorLegs.Play("AndandoAgachado");
        

    }

    public void PlayPlayerBaseComArma()
    {
        _animator.Play("PlayerBaseComArma");
        _animatorLegs.Play("PeAndando");
    }

    public void PlayDesarmado()
    {
        _animator.Play("Desarmado");
        _animatorLegs.Play("PeAndando");
    }

    public void PlayInteract()
    {
        _animator.Play("Interact");
        _animatorLegs.Play("PeIdle");
    }

    public void PlayRunning()
    {
        _animator.Play("Running");
        _animatorLegs.Play("PeCorrendo");
    }

    public void PlayRunningComArma()
    {
        _animator.Play("RunningComArma");
        _animatorLegs.Play("PeCorrendo");
    }

    public void Reload()
    {
        _animator.Play("Reload");
        _animatorLegs.Play("PeIdle");
    }

    public void Shoot()
    {
        _animator.Play("Shoot");
        _animatorLegs.Play("PeIdle");
    }

    public bool IsNotPlayingAnimation(string animationName)
    {
        bool returnValue = true;
        foreach (var animation in _animator.GetCurrentAnimatorClipInfo(0))
        {
            if (animationName == animation.clip.name)
            {
                returnValue = false;
            }
        }

        return returnValue;
    }

    public bool IsPlayingAnimation()
    {
        bool returnValue = false;
        foreach (var animation in _animator.GetCurrentAnimatorClipInfo(0))
        {
            switch (animation.clip.name)
            {
                case "Reload":
                    returnValue = true;
                    break;
                case "Shoot":
                    returnValue = true;
                    break;
                case "Interact":
                    returnValue = true;
                    break;
                case "Attack":
                    returnValue = true;
                    break;
                default:
                    returnValue = false;
                    break;
            }
        }

        return returnValue;
    }

}
