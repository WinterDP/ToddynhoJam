using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AmmoUI : MonoBehaviour
{
    private TextMeshProUGUI _textComponent;

    public static Action<int> OnAmmoUpdate;

    void Awake()
    {
        _textComponent = GetComponent<TextMeshProUGUI>();
    }

    private void OnEnable()
    {
        OnAmmoUpdate += AmmoUpdate;
    }

    private void OnDisable()
    {
        OnAmmoUpdate -= AmmoUpdate;
    }

    private void AmmoUpdate(int currentAmmoValue)
    {
        _textComponent.text = currentAmmoValue + "/12";
    }
}
