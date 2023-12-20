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

    #region Variáveis: Tiro
    [Header("Ataque a distancia")]
    [SerializeField]
    private float _shootSpeed;
    public float ShootSpeed
    {
        get => _shootSpeed;
    }


    [SerializeField]
    private GameObject _shootBulletTrailPrefab;
    public GameObject ShootBulletTrailPrefab
    {
        get => _shootBulletTrailPrefab;
    }


    [SerializeField]
    private float _shootRange;
    public float ShootRange
    {
        get => _shootRange;
    }


    [SerializeField]
    private float _shootCooldown;
    public float ShootCooldown
    {
        get => _shootCooldown;
    }

    [SerializeField]
    private int _maxWeaponAmmo;
    public int MaxWeaponAmmo
    {
        get => _maxWeaponAmmo;
        set => _maxWeaponAmmo = value;
    }

    [SerializeField]
    private int _maxWeaponAmmoPerClip;
    public int MaxWeaponAmmoPerClip
    {
        get => _maxWeaponAmmoPerClip;
    }


    [SerializeField]
    private int _currentWeaponAmmo;
    public int CurrentWeaponAmmo
    {
        get => _currentWeaponAmmo;
        set => _currentWeaponAmmo = value;
    }

    [SerializeField]
    private int _numberOfProjectiles;
    public int NumberOfProjectiles
    {
        get => _numberOfProjectiles;
    }

    [SerializeField]
    private float _recoilMaxAngle;
    public float RecoilMaxAngle
    {
        get => _recoilMaxAngle;
    }

    [SerializeField]
    private float _recoilMinAngle;
    public float RecoilMinAngle
    {
        get => _recoilMinAngle;
    }

    [SerializeField]
    private float _recoilIncrease;
    public float RecoilIncrease
    {
        get => _recoilIncrease;
    }

    [SerializeField]
    private float _shakeCameraStrengh;
    public float ShakeCameraStrengh
    {
        get => _shakeCameraStrengh;
    }
    #endregion
}
