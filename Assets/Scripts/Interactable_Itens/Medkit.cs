using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Medkit : InteractableItem
{
    [SerializeField] private int healAmount;
    private bool interacted = false;
    public override void InteractWithItem(Collider2D collision)
    {
        Debug.Log(collision.gameObject.name + " Curou");
        ItemInteracted();
        
    }

    public override void ItemInteracted()
    {
        if (interacted)
            return;
        interacted = true;
        GameManager.Instance.GetPlayerReference().GetComponent<UnitHealth>().Heal(healAmount);
        Destroy(gameObject);
    }
}
