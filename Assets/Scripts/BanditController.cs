using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BanditController : MonoBehaviour
{
    [SerializeField] private StatsHealhPoints _hpStats;
    [SerializeField] private NavMeshAgent _navAgent;

    [SerializeField] private float navTimer = 5;

    [SerializeField] private float timeDelta;

    [SerializeField] public GameObject banditObjective;

    [SerializeField] private WeaponHandler _weaponHandler;

    [SerializeField] private float distanceDelta;

    [SerializeField] private float attackTimeDelta;

    [SerializeField] private Vector3 lastPosition;

    private Animator _animator;

    // Start is called before the first frame update
    void Start()
    {
        if (TryGetComponent(out StatsHealhPoints HPStats))
        {
            _hpStats = HPStats;
        }

        if (TryGetComponent(out NavMeshAgent navAgent))
        {
            _navAgent = navAgent;
        }

        if (TryGetComponent(out WeaponHandler weaponHandler))
        {
            _weaponHandler = weaponHandler;
        }

        if (TryGetComponent(out Animator animator))
        {
            _animator = animator;
        }
    }

    // Update is called once per frame

    void Update()
    {
        if (_hpStats.CurrentHealth <= 0)
        {
            HandleDeath();
        }

        if ((lastPosition - transform.position).magnitude >= 0.01)
        {
            _animator.SetFloat("Speed", 0.5f);
        }
        else
        {
            _animator.SetFloat("Speed", 0.0f);
        }

        timeDelta += Time.deltaTime;
        UpdateTragetPosition();
        
        
        Attack();
    }
    
    void Attack()
    {
        if ((transform.position - _navAgent.destination).magnitude <= 3)
        {
            if (attackTimeDelta < 1.5f)
            {
                attackTimeDelta += Time.deltaTime;
                return;
            }

            _weaponHandler.Attack();
            attackTimeDelta = 0;
        }
    }

    void CheckAggro()
    {
        if (timeDelta < 3)
        {
            timeDelta += Time.deltaTime;
            return;
        }
        
        
    }

    void UpdateTragetPosition()
    {
        _navAgent.destination = banditObjective.transform.position;
    }

    void HandleDeath()
    {
        // Handle death
    }
}