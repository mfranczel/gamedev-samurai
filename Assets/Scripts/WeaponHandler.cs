using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class WeaponHandler : MonoBehaviour
{
    public GameObject weaponHolder;
    public GameObject weapon;

    [SerializeField] private GameObject currentWeapon;

    [SerializeField] private Animator _animator;

    [SerializeField] private int damage;

    [SerializeField] private bool isAttacking;
    [SerializeField] private float timeDelta;

    // Start is called before the first frame update
    void Start()
    {
        currentWeapon = Instantiate(weapon, weaponHolder.transform);
        currentWeapon.transform.localPosition = Vector3.zero;
        currentWeapon.transform.localScale = new Vector3(100, 100, 100);

        if (gameObject.TryGetComponent(out Animator animator))
        {
            _animator = animator;
        }

        damage = 20;
    }

    // Update is called once per frame
    void Update()
    {
        if (isAttacking)
        {
            if (timeDelta > 1)
            {
                isAttacking = false;
                timeDelta = 0;
            }
            else
            {
                timeDelta += Time.deltaTime;
            }
        }
    }

    public void Attack()
    {
        isAttacking = true;
        _animator.SetTrigger("Attack");
    }

    void OnCollisionEnter(Collision col)
    {
        
        print(col.gameObject.name);
        if (!isAttacking)
        {
            return;
        }

        if (gameObject.CompareTag("Player"))
        {
            if (col.gameObject.CompareTag("Bandits"))
            {
                print(col.gameObject.name);
                if (col.gameObject.TryGetComponent(out StatsHealhPoints healthPoints))
                {
                    healthPoints.TakeDamage(damage);
                }
            }
        }
        
        if (gameObject.CompareTag("Bandits"))
        {
            if (col.gameObject.CompareTag("Villagers"))
            {
                print(col.gameObject.name);
                if (col.gameObject.TryGetComponent(out StatsHealhPoints healthPoints))
                {
                    healthPoints.TakeDamage(damage);
                }
            }

            if (col.gameObject.CompareTag("Player"))
            {
                if (col.gameObject.TryGetComponent(out PlayerController playerController))
                {
                    playerController.TakeDamage(damage);
                }
            }
            
        }
    }
}