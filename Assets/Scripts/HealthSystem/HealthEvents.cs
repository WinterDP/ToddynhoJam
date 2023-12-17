using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthEvents
{
    public Action<int> OnTakeDamage;
    public Action<int> OnUseHealthItem;
    public Action OnUnitDied;
    public Action<int> OnUnitSpawn;

    private int _maxHealth;
    public int CurrentHealth { get; private set; }

    public void UnitSpawn(int maxHealth) { _maxHealth = maxHealth; OnUnitSpawn?.Invoke(maxHealth); }
    public void UnitDied() { OnUnitDied?.Invoke(); }

    public void TakeDamage(int amount) 
    {
        CurrentHealth -= amount;
        OnTakeDamage?.Invoke(amount);
        if (CurrentHealth <= 0f)
            UnitDied();
    }
    public void UseHealthItem(int amount) 
    { 
        CurrentHealth += amount;
        OnUseHealthItem?.Invoke(amount);
    }  
}
