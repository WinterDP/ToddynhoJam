using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoBox : InteractableItem
{
    [SerializeField]
    private int _ammoAmount;
    public override void InteractWithItem(Collider2D collision)
    {

        collision.gameObject.GetComponentInChildren<PlayerAttack>().CurrentWeapon.MaxWeaponAmmo += _ammoAmount;
        ItemInteracted();
    }

    public override void ItemInteracted()
    {
        Destroy(gameObject);
    }
}

