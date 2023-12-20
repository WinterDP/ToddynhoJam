using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Battery : InteractableItem
{
    [SerializeField]
    private float _batteryRechargeValue;
    public override void InteractWithItem(Collider2D collision)
    {
        collision.gameObject.GetComponentInChildren<LanternHandler>().ChargeBattery(_batteryRechargeValue);
        ItemInteracted();
    }

    public override void ItemInteracted()
    {
        Destroy(gameObject);
    }
}
