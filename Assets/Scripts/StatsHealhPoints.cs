using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class StatsHealhPoints : MonoBehaviour
{
    public int MaxHealth = 100;
    public int BaseHealth = 100;

    public int CurrentHealth;

    public int Upgrades = 0;

    public int UpgradeValue = 2;
    // Start is called before the first frame update
    void Start()
    {
        UpdateStats();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void UpdateStats()
    {
        MaxHealth = BaseHealth + (Upgrades * UpgradeValue);
        CurrentHealth = MaxHealth;
    }

    public void TakeDamage(int value)
    {
        CurrentHealth -= value;
        if (CurrentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }
}
