using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Medkit : InteractableItem
{
    public override void InteractWithItem(Collider2D collision)
    {
        Debug.Log(collision.gameObject.name + " Curou");
        ItemInteracted();
        
    }

    public override void ItemInteracted()
    {
        Destroy(gameObject);
    }
}
