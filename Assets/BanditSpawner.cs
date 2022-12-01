using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BanditSpawner : MonoBehaviour
{
    public GameObject Bandit;

    public GameObject Spawnpoint;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 5; i++)
        {
            Vector3 pos = Spawnpoint.transform.position;
            pos.x += Random.Range(-5, 5);
            pos.z += Random.Range(-5, 5);
            GameObject newBandit = Instantiate(Bandit, pos, Quaternion.identity);
            newBandit.SetActive(true);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
