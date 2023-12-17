using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New-Weapon-Scriptable-Object", menuName = "Itens/Weapons")]
public class WeaponSO : ScriptableObject
{
    #region Variáveis: Ataque Melee

    [Header("Ataque Melee")]
    [SerializeField]
    private float _meleeAttackDamage;
    public float MeleeAttackDamage
    {
        get => _meleeAttackDamage;
    }
    [SerializeField]
    private float _meleeAttackCooldown;
    public float MeleeAttackCooldown
    {
        get => _meleeAttackCooldown;
    }
    [SerializeField]
    private float _meleeAttackRange;
    public float MeleeAtackRange
    {
        get => _meleeAttackRange;
    }

    #endregion
}
