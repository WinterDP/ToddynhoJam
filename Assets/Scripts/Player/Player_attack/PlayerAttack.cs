using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    #region Variáveis: configuração geral
    [SerializeField]
    private WeaponSO _currentWeapon;
    [SerializeField]
    private LayerMask _whatIsEnemy;
    [SerializeField]
    private CameraShakeController _cameraShakeControllerReference;

    private StateHandler _stateHandlerReference;
    #endregion

    #region Variáveis: Melee
    [Header("Ataque Melee")]
    private bool _isMeleeAttacking;
    public bool IsMeleeAttacking
    {
        get => _isMeleeAttacking;
        set => _isMeleeAttacking = value;
    }

    [SerializeField]
    private Transform _attackPosition;

    private float _currentMeleeCooldown;
    #endregion

    #region Variáveis: Tiro
    [Header("Ataque a distancia")]
    private bool _isFiring;
    public bool IsFiring
    {
        get => _isFiring;
        set => _isFiring = value;
    }
    private float _currentShootCooldown;
    [SerializeField]
    private Transform _firePoint;
    [SerializeField]
    private LayerMask _IgnoreLayers;

    private float _currentAngleRecoil;
    #endregion

    private void Awake()
    {
        _stateHandlerReference = gameObject.GetComponent<StateHandler>();
    }

    // Start is called before the first frame update
    void Start()
    {
        _currentAngleRecoil = _currentWeapon.RecoilMinAngle;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        MeleeAttack();
        Shoot();
    }

    public void MeleeAttack()
    {
        if (_currentMeleeCooldown > 0)
        {
            _currentMeleeCooldown -= Time.deltaTime;
        }
        else
        {
            if (_isMeleeAttacking)
            {
                Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(_attackPosition.position, _currentWeapon.MeleeAtackRange, _whatIsEnemy);

                foreach (Collider2D enemy in enemiesToDamage)
                {
                    Debug.Log("atingiu " + enemy.gameObject.name);
                }

                _isMeleeAttacking = false;
                _currentMeleeCooldown = _currentWeapon.MeleeAttackCooldown;
            }
        }
    }

    private void Shoot()
    {
        if (_currentShootCooldown > 0)
        {
            _currentShootCooldown -= Time.deltaTime;
        }
        else
        {
            if (_isFiring)
            {
                for (int i = 0; i < _currentWeapon.NumberOfProjectiles; i++)
                {
                    Vector3 shootingDirection = HandleRecoil();

                    RaycastHit2D ray = Physics2D.Raycast(_firePoint.position, shootingDirection, _currentWeapon.ShootRange, ~(_IgnoreLayers));

                    GameObject trail = Instantiate(_currentWeapon.ShootBulletTrailPrefab, _firePoint.position, transform.rotation);

                    BulletTrail bulletTrailReference = trail.GetComponent<BulletTrail>();

                    if (ray.collider != null)
                    {
                        bulletTrailReference.SetTargetPosition(ray.point);
                        Debug.Log(ray.collider.gameObject.name);
                        // lógica de acertar algo
                    }
                    else
                    {
                        Vector2 endPoint = _firePoint.position + shootingDirection * _currentWeapon.ShootRange;

                        bulletTrailReference.SetTargetPosition(endPoint);
                    }

                }

                _cameraShakeControllerReference.ShakeCamera(_currentWeapon.ShakeCameraStrengh, 0.1f);
                _currentShootCooldown = _currentWeapon.ShootCooldown;
            }
            else
            {
                if (!_stateHandlerReference.IsRunning)
                {
                    _currentAngleRecoil = _currentWeapon.RecoilMinAngle;
                }
            }
        }
    }

    public Vector3 HandleRecoil()
    {

        CalculateRecoil();

        float AngleRecoil = Random.Range(_currentAngleRecoil, -_currentAngleRecoil);

        return Quaternion.AngleAxis(AngleRecoil, Vector3.forward) * transform.right;
    }

    public void CalculateRecoil()
    {
        if (_currentAngleRecoil < _currentWeapon.RecoilMaxAngle)
        {
            _currentAngleRecoil += _currentWeapon.RecoilIncrease * Time.deltaTime;
        }
        else
        {
            _currentAngleRecoil = _currentWeapon.RecoilMaxAngle;
        }
    }

    #if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(_attackPosition.position, _currentWeapon.MeleeAtackRange);
    }
    #endif
}