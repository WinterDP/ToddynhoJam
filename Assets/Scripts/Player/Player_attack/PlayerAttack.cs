using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private bool _isMeleeAttacking;
    public bool IsMeleeAttacking
    {
        get => _isMeleeAttacking;
        set => _isMeleeAttacking = value;
    }


    [SerializeField]
    private WeaponSO _currentWeapon;
    [SerializeField]
    private Transform _attackPosition;
    [SerializeField]
    private LayerMask _whatIsEnemy;
    private float _currentCooldown;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        MeleeAttack();
    }

    public void MeleeAttack()
    {
        if(_currentCooldown > 0)
        {
            _currentCooldown -= Time.deltaTime;
        }
        else
        {
            if (_isMeleeAttacking)
            {
                Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(_attackPosition.position, _currentWeapon.MeleeAtackRange, _whatIsEnemy);

                foreach (Collider2D enemy in enemiesToDamage)
                {
                    Debug.Log("atingiu "+enemy.gameObject.name);
                }

                _isMeleeAttacking = false;
                _currentCooldown = _currentWeapon.MeleeAttackCooldown;
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(_attackPosition.position, _currentWeapon.MeleeAtackRange);
    }
}
