using System;
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
    private GameObject navTarget;

    [SerializeField] private WeaponHandler _weaponHandler;

    [SerializeField] private float distanceDelta;

    [SerializeField] private float attackTimeDelta;

    [SerializeField] private Vector3 lastPosition;
    public BanditSpawner banditSpawner;

    public GameObject _player;
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

        _player = GameObject.FindWithTag("Player");
        navTarget = banditObjective;

    }

    // Update is called once per frame

    void Update()
    {
        if ((lastPosition - transform.position).magnitude >= 0.01)
        {
            _animator.SetFloat("Speed", 0.5f);
        }
        else
        {
            _animator.SetFloat("Speed", 0.0f);
        }

        timeDelta += Time.deltaTime;
        UpdateTargetPosition();
        
        
        Attack();
        CheckAggro();
        
        HandleDeath();
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

        if (ReferenceEquals(banditObjective, null))
        {
            navTarget = _player;
        }
        
        if ((_player.transform.position - transform.position).magnitude <= 10)
        {
            navTarget = _player;
        }
        else
        {
            
            navTarget = banditObjective;
        }

        timeDelta = 0;
    }

    void UpdateTargetPosition()
    {
        _navAgent.destination = navTarget.transform.position;
    }

    public void HandleDeath()
    {
        if (_hpStats.CurrentHealth <= 0)
        {
            if (_player.TryGetComponent(out PlayerController playerController))
            {
                playerController.AddKill(1);
            }
            
            if (banditSpawner.TryGetComponent(out BanditSpawner spawner))
            {
                spawner.lastBanditCount--;
            }
            Destroy(gameObject);
        }
    }
}