using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class PlayerHealthFeedback : MonoBehaviour
{
    private Volume volume;
    private Vignette vignette;

    private float maxIntensity;

    [SerializeField] private UnitHealth playerHealth;

    private void Awake()
    {
        volume = GetComponent<Volume>();
        volume.profile.TryGet(out vignette);
        maxIntensity = (float)vignette.intensity;
        vignette.intensity.Override(0f);

    }

    private void OnEnable()
    {
        playerHealth.HealthEvents.OnTakeDamage += TakeDamage;
        playerHealth.HealthEvents.OnUseHealthItem += Heal;
    }

    private void OnDisable()
    {
        playerHealth.HealthEvents.OnTakeDamage -= TakeDamage;
        playerHealth.HealthEvents.OnUseHealthItem -= Heal;
    }

    private void Heal(int amount)
    { 
        float playerHealthNormalized = playerHealth.HealthEvents.CurrentHealth / playerHealth.MaxHealth;
        vignette.intensity.Override(playerHealthNormalized * maxIntensity);
    }
    private void TakeDamage(int amount) 
    {
        float playerHealthNormalized = playerHealth.HealthEvents.CurrentHealth / playerHealth.MaxHealth;
        vignette.intensity.Override(playerHealthNormalized * maxIntensity);

    }

}
