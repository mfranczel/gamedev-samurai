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

    public int WaveNumber = 1;
    // Start is called before the first frame update
    void Start()
    {
        int BanditCount = WaveNumber * 2 + 5;
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
            
            newBandit.SetActive(true);
            if (newBandit.TryGetComponent(out NavMeshAgent agent))
            {
                agent.destination =  Target.transform.position;
            }
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
