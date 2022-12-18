using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class VillagerController : MonoBehaviour
{
    // Start is called before the first frame update
    public List<GameObject> VillagerTargets;

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (TryGetComponent(out NavMeshAgent agent))
            {
                float dist = agent.remainingDistance;
                if (agent.pathStatus == NavMeshPathStatus.PathComplete) // dist != Mathf.Infinity && agent.pathStatus == NavMeshPathStatus.PathComplete &&agent.remainingDistance == 0
                {
                    // agent.destination = VillagerTargets[Random.Range(0, VillagerTargets.Count)].transform.position;
                }
            }
    }
}