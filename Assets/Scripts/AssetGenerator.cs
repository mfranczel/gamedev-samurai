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

    // Start is called before the first frame update
    public void GenerateAssets()
    {
        if (spawnedObjects.Count > 0) {
            destroyAll();
        }

        mapWidth = map.bounds.extents.x;
        mapLength = map.bounds.extents.z;

        numberOfObjectsToSpawn = Random.Range(minNumberToSpawn, maxNumberToSpawn + 1);

        for (int i = 0; i < numberOfObjectsToSpawn; i++)
        {
            go = Instantiate(prefabs[Random.Range(0, prefabs.Count)]);
            tr = go.transform;
            col = go.GetComponent<Collider>();

            bool foundHit = false;
            Vector3 hitpoint = new Vector3();

            float y = maxHeight; // How high we start from, 10 meters in this case
            do
            {
                float randomX = Random.Range(-mapWidth, mapWidth);
                float randomZ = Random.Range(-mapLength, mapLength);

                if (Mathf.Sqrt(Mathf.Pow(randomX, 2) + Mathf.Pow(randomZ, 2)) >= cityRadius) {
                    Vector3 testPosition = new Vector3(randomX, y, randomZ);

                    // Perform a raycast
                    RaycastHit hit;
                    if (Physics.Raycast(testPosition, Vector3.down, out hit, Mathf.Infinity))
                    {
                        // If we hit object tagged as "Ground"
                        if (hit.transform.gameObject.tag == "Ground" && hit.point.y > minHeight)
                        {
                            Debug.DrawRay(testPosition, Vector3.down * hit.distance, Color.green, 1f);
                            Debug.Log("Hit Ground");
                            foundHit = true;
                            hitpoint = hit.point;
                        }
                        // If not hit something else withing bounds.
                        else
                        {
                            Debug.DrawRay(testPosition, Vector3.down * hit.distance, Color.red, 1f);
                            Debug.Log("Hit something else " + hit.transform.gameObject.tag);
                        }
                    }
                }

            } while (!foundHit);

            tr.position = hitpoint;

            /*do
            {
                tr.position = new Vector3(Random.Range(-mapWidth, mapWidth), map.transform.position.y + col.bounds.extents.y, Random.Range(-mapLength, mapLength));
            }
            while (IsInBuildingOrInSpawnedObject(col));*/
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
