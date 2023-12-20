using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lantern : InteractableItem
{
    public override void InteractWithItem(Collider2D collision)
    {
        collision.gameObject.GetComponent<StateHandler>().HasLantern = true;
        ItemInteracted();
    }

    public override void ItemInteracted()
    {
        Destroy(gameObject);
    }
}
