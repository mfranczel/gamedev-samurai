using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
