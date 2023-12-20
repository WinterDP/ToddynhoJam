using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Medkit : InteractableItem
{
    [SerializeField] private int healAmount;
    public override void InteractWithItem(Collider2D collision)
    {
        Debug.Log(collision.gameObject.name + " Curou");
        ItemInteracted();
        
    }

    public override void ItemInteracted()
    {
        GameManager.Instance.GetPlayerReference().GetComponent<UnitHealth>().Heal(healAmount);
        Destroy(gameObject);
    }
}
