using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BanditSpawner : MonoBehaviour
{
    public GameObject Bandit;
    public GameObject Target;
    public GameObject Player;

    public GameObject Spawnpoint;
    public int lastBanditCount;
    private float timeDelta;

    public int WaveNumber = 1;

    public bool waveDestoyed;
    // Start is called before the first frame update
    void Start()
    {
        SpawnWave();
    }

    // Update is called once per frame
    void Update()
    {
        if (lastBanditCount <= 0)
        {
            waveDestoyed = true;
        }

        if (waveDestoyed && Input.GetKeyDown("Enter"))
        {
            SpawnWave();
        }
    }

    void SpawnWave()
    {
        int BanditCount = WaveNumber * 2 + 1;
        for (int i = 0; i < BanditCount; i++)
        {
            Vector3 pos = Spawnpoint.transform.position;
            pos.x += Random.Range(-5, 5);
            pos.z += Random.Range(-5, 5);
            GameObject newBandit = Instantiate(Bandit, pos, Quaternion.identity);
            
            if (newBandit.TryGetComponent(out StatsHealhPoints HPStats))
            {
                HPStats.Upgrades = WaveNumber * 2;
            }
            
            if (newBandit.TryGetComponent(out BanditController banditController))
            {
                banditController.banditObjective = Target;
            }
            
            newBandit.SetActive(true);
            if (newBandit.TryGetComponent(out NavMeshAgent agent))
            {
                agent.destination =  Target.transform.position;
            }
        }

        lastBanditCount = BanditCount;
        WaveNumber++;
        waveDestoyed = false;
    }
    
}
