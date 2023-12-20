using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoBox : InteractableItem
{
    [SerializeField]
    private int _ammoAmount;
    public override void InteractWithItem(Collider2D collision)
    {
        PlayerAttack playerAttackReference = collision.gameObject.GetComponentInChildren<PlayerAttack>();
        playerAttackReference.CurrentWeapon.MaxWeaponAmmo += _ammoAmount;
        playerAttackReference.AmmoUIReference.AmmoUpdate();
        ItemInteracted();
    }

    public override void ItemInteracted()
    {
        Destroy(gameObject);
    }
}

