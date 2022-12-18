using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class VillagerSpawner : MonoBehaviour
{
    public GameObject Villager;
    public List<GameObject> VillagerTargets;

    public GameObject Spawnpoint;

    public int VillagerNumber = 1;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < VillagerNumber; i++)
        {
            Vector3 pos = Spawnpoint.transform.position;
            pos.x += Random.Range(-1, 1);
            pos.z += Random.Range(-1, 1);
            GameObject newVillager = Instantiate(Villager, pos, Quaternion.identity);
            if (newVillager.TryGetComponent(out VillagerController villagerController))
            {
                villagerController.VillagerTargets = VillagerTargets;
            }
            
            
            if (newVillager.TryGetComponent(out StatsHealhPoints HPStats))
            {
                HPStats.Upgrades = 0;
            }
            
            newVillager.SetActive(true);
            if (newVillager.TryGetComponent(out NavMeshAgent agent))
            {
                agent.destination =  VillagerTargets[Random.Range(0, VillagerTargets.Count)].transform.position;
            }
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}