using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StaminaFeedback : MonoBehaviour
{
    private PlayerStamina playerStamina;
    private Slider staminaSlider;

    private void Awake()
    {
        staminaSlider = GetComponentInChildren<Slider>();
    }
    void Start()
    {
        playerStamina = GetComponent<PlayerStamina>();
        staminaSlider.value = playerStamina.MaxStamina;
    }

    public void UpdateStaminaBar(float currentValue)
    {
        staminaSlider.value = currentValue;
    }

}
