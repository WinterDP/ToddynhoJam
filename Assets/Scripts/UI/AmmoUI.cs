using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AmmoUI : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _textComponent;

    private PlayerAttack _playerAttackReference;

    public static Action<int> OnAmmoUpdate;

    void Awake()
    {

        _playerAttackReference = gameObject.GetComponent<PlayerAttack>();
    }

    public void AmmoUpdate()
    {
        if (_playerAttackReference.CurrentWeapon != null)
        {
            _textComponent.text = _playerAttackReference.CurrentWeapon.CurrentWeaponAmmo + "/" + _playerAttackReference.CurrentWeapon.MaxWeaponAmmo;
        }
        else
        {
            _textComponent.text = "";
        }
        
    }
}
