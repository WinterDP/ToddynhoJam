using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TesteRecarrega : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponentInChildren<LanternHandler>() != null)
        {
            collision.gameObject.GetComponentInChildren<LanternHandler>().ChargeBattery(50f);
        }
    }
}
