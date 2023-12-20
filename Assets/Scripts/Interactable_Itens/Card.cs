using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : InteractableItem
{
    [SerializeField]
    private CardColor _CardType;

    public override void InteractWithItem(Collider2D collision)
    {
        switch (_CardType)
        {
            case CardColor.BLUE:
                collision.gameObject.GetComponent<StateHandler>().HasCardBlue = true;
                break;
            case CardColor.PURPLE:
                collision.gameObject.GetComponent<StateHandler>().HasCardPurple = true;
                break;
            case CardColor.GRAY:
                collision.gameObject.GetComponent<StateHandler>().HasCardGray = true;
                break;
            default:
                break;
        }
        
        ItemInteracted();
    }

    public override void ItemInteracted()
    {
        Destroy(gameObject);
    }
}

enum CardColor
{
    BLUE,
    PURPLE,
    GRAY
}