using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : InteractableItem
{
    private bool _canOpenDoor = false;
    enum CardColor
    {
        NONE,
        BLUE,
        PURPLE,
        GRAY
    }

    [SerializeField] private CardColor cardColorToOpen;

    public override void InteractWithItem(Collider2D collision)
    {
        var stateHandler = GameManager.Instance.GetPlayerReference().GetComponent<StateHandler>();
        switch(cardColorToOpen)
        {
            case CardColor.NONE:
                _canOpenDoor = true;
                break;
            case CardColor.BLUE:
                _canOpenDoor = (stateHandler.HasCardBlue) ? true : false;
                stateHandler.HasCardBlue = _canOpenDoor ? false : true;
                break;
            case CardColor.PURPLE:
                _canOpenDoor = (stateHandler.HasCardPurple) ? true : false;
                stateHandler.HasCardPurple = _canOpenDoor ? false : true;
                break;
            case CardColor.GRAY:
                _canOpenDoor = (stateHandler.HasCardGray) ? true : false;
                stateHandler.HasCardGray = _canOpenDoor ? false : true;
                break;
        }
    }

    public override void ItemInteracted()
    {
        if(_canOpenDoor)
            Destroy(gameObject);
    }

}
