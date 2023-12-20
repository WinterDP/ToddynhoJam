using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public abstract class InteractableItem : MonoBehaviour
{
    [SerializeField]
    private bool _needTimeToInteract;

    [SerializeField]
    private float _timeToInteract;
    private float _currentTimeInteracting;
    private LanternHandler _lanterHandlerReference;
    private bool _currentLanternState;

    // Start is called before the first frame update
    void Start()
    {
        _currentTimeInteracting = _timeToInteract;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        // Caso o Player entre na �rea de colis�o do item, ele pode interagir com ele

        // S�o pegadas as referencias de alguns scripts do player para a altera��o de dados no player
        InputHandler inputHandlerReference = collision.gameObject.GetComponent<InputHandler>();
        StateHandler stateHandlerReference = collision.gameObject.GetComponent<StateHandler>();
        FeedbackInteractableItem feedbackInteractableItemReference = collision.gameObject.GetComponent<FeedbackInteractableItem>();
        if (_currentTimeInteracting == _timeToInteract)
        {
            _lanterHandlerReference = collision.gameObject.GetComponentInChildren<LanternHandler>();
            _currentLanternState = _lanterHandlerReference.IsLanternTurnedOn;
        }
        

        // Caso o player realiza a intera��o pelo input
        if (inputHandlerReference != null && stateHandlerReference != null && inputHandlerReference.IsInteracting)
        {
            // Verifica-se o item precisa de tempo para que a intera��o seja realizada
            stateHandlerReference.IsInteracting = true;
            if (_needTimeToInteract)
            {
                if (_currentLanternState)
                    _lanterHandlerReference.TurnOffLantern();

                if (_currentTimeInteracting >= 0)
                {
                    _currentTimeInteracting -= Time.deltaTime;

                    feedbackInteractableItemReference.UpdateInteractableBar(_currentTimeInteracting,  _timeToInteract);
                    // Caso o player realize alguma a��o a itera��o � encerrada
                    if (stateHandlerReference.IsWalkingBackward || stateHandlerReference.IsWalkingFoward || stateHandlerReference.IsCrouching || stateHandlerReference.IsRunning || stateHandlerReference.IsShooting)
                    {
                        if (_currentLanternState)
                            _lanterHandlerReference.TurnOnLantern();

                        _currentTimeInteracting = _timeToInteract;
                        stateHandlerReference.IsInteracting = false;
                        inputHandlerReference.IsInteracting = false;
                        feedbackInteractableItemReference.UpdateInteractableBar(_currentTimeInteracting, _timeToInteract);
                    }

                }
                else
                {
                    // Ao fim do tempo de intera��o o player interage com o item
                    if (_currentLanternState)
                        _lanterHandlerReference.TurnOnLantern();
                    InteractWithItem(collision);
                    _currentTimeInteracting = _timeToInteract;
                    stateHandlerReference.IsInteracting = false;
                    inputHandlerReference.IsInteracting = false;
                    feedbackInteractableItemReference.UpdateInteractableBar(_currentTimeInteracting, _timeToInteract);
                }

            }
            else
            {
                // caso n�o seja necess�rio tempo de intera��o o player interage de imediato
                InteractWithItem(collision);
                stateHandlerReference.IsInteracting = false;
                inputHandlerReference.IsInteracting = false;
            }
        }
    }

    public abstract void InteractWithItem(Collider2D collision);

    public abstract void ItemInteracted();
}
