using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssetGenerator : MonoBehaviour
{
    public List<GameObject> prefabs = new List<GameObject>();
    public Collider map;

    int numberOfObjectsToSpawn;
    public int minNumberToSpawn = 10;
    public int maxNumberToSpawn = 50;
    public float cityRadius;
    public float minHeight = 32f;
    public float maxHeight = 70f;
    public bool autoUpdate;

    float mapWidth;
    float mapLength;

    GameObject go;
    Transform tr;
    Collider col;
    List<Collider> spawnedObjects = new List<Collider>();
    List<GameObject> spawnedGameObjects = new List<GameObject>();


    public void GenerateAssets()
    {
        // Destroy all if already spawned
        if (spawnedObjects.Count > 0) {
            destroyAll();
        }

        mapWidth = map.bounds.extents.x;
        mapLength = map.bounds.extents.z;

        numberOfObjectsToSpawn = Random.Range(minNumberToSpawn, maxNumberToSpawn + 1);

        // Generate number (between minNumberToSpawn and maxNumberToSpawn) of random objects 
        for (int i = 0; i < numberOfObjectsToSpawn; i++)
        {
            go = Instantiate(prefabs[Random.Range(0, prefabs.Count)]);
            tr = go.transform;
            col = go.GetComponent<Collider>();

            bool foundHit = false;
            Vector3 hitpoint = new Vector3();
            float randomX;
            float randomZ;

            float y = maxHeight; // How high we start from, y meters
            do
            {
               randomX = Random.Range(-mapWidth, mapWidth);
                randomZ = Random.Range(-mapLength, mapLength);

                if (Mathf.Sqrt(Mathf.Pow(randomX, 2) + Mathf.Pow(randomZ, 2)) >= cityRadius) {
                    Vector3 testPosition = new Vector3(randomX, y, randomZ);

                    // Perform a raycast to find out, if position suitable for object
                    RaycastHit hit;
                    if (Physics.Raycast(testPosition, Vector3.down, out hit, Mathf.Infinity))
                    {
                        // If we hit object tagged as "Ground"
                        if (hit.transform.gameObject.tag == "Ground" && hit.point.y > minHeight)
                        {
                            Debug.Log("Hit Ground");
                            foundHit = true;
                            hitpoint = hit.point;
                        }
                        // If not hit something else withing bounds.
                        else
                        {
                            Debug.Log("Hit something else " + hit.transform.gameObject.tag);
                        }
                    }
                }

            } while (!foundHit);

            Vector3 newPosition = new Vector3(randomX, hitpoint.y, randomZ);

            tr.position = newPosition;
            spawnedObjects.Add(col);
            spawnedGameObjects.Add(go);
        }
    }

    private void destroyAll() {
        for (int i = 0; i < spawnedGameObjects.Count; i++) {
            DestroyImmediate(spawnedGameObjects[i]);
        }
        spawnedObjects = new List<Collider>();
    }

    private bool IsInBuildingOrInSpawnedObject(Collider c)
    {
        bool b = false;

        for (int j = 0; j < spawnedObjects.Count; j++)
        {
            if (spawnedObjects[j] != c)
            {
                b = spawnedObjects[j].bounds.Contains(c.ClosestPoint(spawnedObjects[j].transform.position));

                if (b)
                {
                    return b;
                }
            }
        }

        return b;
    }

}
