using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitHealth : MonoBehaviour
{
    private HealthEvents _healthEvents;
    public HealthEvents HealthEvents => _healthEvents;

    [SerializeField] int _maxHealth;
    public int MaxHealth => _maxHealth;

    private void Awake()
    {
        _healthEvents = new HealthEvents();
        Setup(_maxHealth);
    }

    private void OnEnable()
    {
        _healthEvents.OnTakeDamage += TakeDamage;
        _healthEvents.OnUseHealthItem += Heal;
        _healthEvents.OnUnitSpawn += Setup;
        _healthEvents.OnUnitDied += UnitDied;
    }
    private void OnDisable()
    {
        _healthEvents.OnTakeDamage -= TakeDamage;
        _healthEvents.OnUseHealthItem -= Heal;
        _healthEvents.OnUnitSpawn -= Setup;
        _healthEvents.OnUnitDied -= UnitDied;
    }

    private void Setup(int maxHealth)
    {
        _healthEvents.UnitSpawn(maxHealth);
    }
    public void Heal(int amount)
    {
        _healthEvents.UseHealthItem(amount);
    }
    public void TakeDamage(int amount)
    {
        _healthEvents.TakeDamage(amount);
    }
    public void UnitDied()
    {
        Destroy(gameObject); //die anim, trigger, wait for secs, destroy
    }

}
