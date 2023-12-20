using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : InteractableItem
{
    [SerializeField]
    private WeaponSO _weapon;
    public override void InteractWithItem(Collider2D collision)
    {
        PlayerAttack playerAttackReference = collision.gameObject.GetComponentInChildren<PlayerAttack>();
        playerAttackReference.CurrentWeapon = _weapon;
        playerAttackReference.AmmoUIReference.AmmoUpdate();
        ItemInteracted();
    }

    public override void ItemInteracted()
    {
        Destroy(gameObject);
    }
}
