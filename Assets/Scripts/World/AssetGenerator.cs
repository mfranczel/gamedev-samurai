using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AssetGenerator : MonoBehaviour
{
    public NavMeshSurface surface;

    public List<GameObject> plantPrefabs = new List<GameObject>();
    public List<GameObject> rockPrefabs = new List<GameObject>();
    public List<GameObject> bushPrefabs = new List<GameObject>();
    public List<GameObject> bambooPrefabs = new List<GameObject>();
    public Collider map;

    public Vector2 plantNumberRange = new Vector2(0, 0);
    public Vector2 rockNumberRange = new Vector2(0, 0);
    public Vector2 bushNumberRange = new Vector2(0, 0);
    public Vector2 bambooNumberRange = new Vector2(0, 0);

    public Vector2 plantHeightRange = new Vector2(0, 0);
    public Vector2 rockHeightRange = new Vector2(0, 0);
    public Vector2 bushHeightRange = new Vector2(0, 0);
    public Vector2 bambooHeightRange = new Vector2(0, 0);

    public float plantCityRadius = 0f;
    public float rockCityRadius = 0f;
    public float bushCityRadius = 0f;
    public float bambooCityRadius = 0f;

    public bool autoUpdate;

    float mapWidth;
    float mapLength;

    GameObject go;
    Transform tr;
    Collider col;
    List<Collider> spawnedObjects = new List<Collider>();
    List<GameObject> spawnedGameObjects = new List<GameObject>();

    void Awake()
    {
        GenerateAllAssets();
    }

    public void ResetAllAssets()
    {
        // Destroy all if already spawned
        if (spawnedObjects.Count > 0) {
            destroyAll();
        }
    }

    public void GenerateAllAssets()
    {
        if (spawnedObjects.Count == 0)
        {
            // generate separately groups of assets
            GenerateAssets(plantPrefabs, plantNumberRange, plantHeightRange, plantCityRadius);
            GenerateAssets(rockPrefabs, rockNumberRange, rockHeightRange, rockCityRadius);
            GenerateAssets(bushPrefabs, bushNumberRange, bushHeightRange, bushCityRadius);
            GenerateAssets(bambooPrefabs, bambooNumberRange, bambooHeightRange, bambooCityRadius);
        }

        BuildNavMesh();
    }

    private void GenerateAssets(List<GameObject> prefabs, Vector2 numberRange, Vector2 heightRange, float cityRadius)
    {

        mapWidth = map.bounds.extents.x;
        mapLength = map.bounds.extents.z;

        int numberOfObjectsToSpawn = Random.Range((int) numberRange.x, (int) numberRange.y + 1);

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

            float y = heightRange.y; // How high we start from, y meters
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
                        if (hit.transform.gameObject.tag == "Ground" && hit.point.y > heightRange.x)
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
        surface.RemoveData();
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
    public void BuildNavMesh()
    {
        Debug.Log("Building navmesh");
        // bake navmesh 
        if (surface.navMeshData == null)
        {
            surface.BuildNavMesh();
        }
    }

}
