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
        // Caso o Player entre na área de colisão do item, ele pode interagir com ele

        // São pegadas as referencias de alguns scripts do player para a alteração de dados no player
        InputHandler inputHandlerReference = collision.gameObject.GetComponent<InputHandler>();
        StateHandler stateHandlerReference = collision.gameObject.GetComponent<StateHandler>();

        // Caso o player realiza a interação pelo input
        if (inputHandlerReference != null && stateHandlerReference != null && inputHandlerReference.IsInteracting)
        {
            // Verifica-se o item precisa de tempo para que a interação seja realizada
            stateHandlerReference.IsInteracting = true;
            if (_needTimeToInteract)
            {
                if (_currentTimeInteracting > 0)
                {
                    _currentTimeInteracting -= Time.deltaTime;

                    // Caso o player realize alguma ação a iteração é encerrada
                    if (stateHandlerReference.IsWalkingBackward || stateHandlerReference.IsWalkingFoward || stateHandlerReference.IsCrouching || stateHandlerReference.IsRunning || stateHandlerReference.IsShooting)
                    {
                        _currentTimeInteracting = _timeToInteract;
                        stateHandlerReference.IsInteracting = false;
                        inputHandlerReference.IsInteracting = false;
                    }
                }
                else
                {
                    // Ao fim do tempo de interação o player interage com o item
                    InteractWithItem(collision);
                    _currentTimeInteracting = _timeToInteract;
                    stateHandlerReference.IsInteracting = false;
                    inputHandlerReference.IsInteracting = false;
                }

            }
            else
            {
                // caso não seja necessário tempo de interação o player interage de imediato
                InteractWithItem(collision);
                stateHandlerReference.IsInteracting = false;
                inputHandlerReference.IsInteracting = false;
            }
        }
    }

    public abstract void InteractWithItem(Collider2D collision);

    public abstract void ItemInteracted();
}
