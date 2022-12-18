using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{

    public enum DrawMode { NoiseMap, ColorMap, Mesh };
    public DrawMode drawMode;
    public int chunkSize = 100;
    [Range(0, 6)]
    public int detailLevel;
    public float noiseScale;

    public bool autoUpdate;

    public int octaves;

    [Range(0, 1)]
    public float persistance;
    public float lacunarity;
    public Vector2 offset;
    public bool isIsland;
    public float landScale;

    public float heightMultiplier;
    public TerrainType[] regions;
    public AnimationCurve heightCurve;

    void Awake()
    {
        GenerateMap();

    }

    public void GenerateMap()
    {
        int seed = OptionsMenu.seed;
        
        float[,] noiseMap = Noise.GenerateNoiseMap(
            chunkSize,
            chunkSize,
            noiseScale,
            octaves,
            persistance,
            lacunarity,
            seed,
            offset,
            isIsland,
            landScale
        );
        
        Color[] colorMap = new Color[chunkSize * chunkSize];

        for (int i = 0, y = 0; y < chunkSize; y++)
        {
            for (int x = 0; x < chunkSize; x++)
            {
                float currentHeight = noiseMap[x, y];
                for (int r = 0; r < regions.Length; r++)
                {
                    if (currentHeight <= regions[r].height)
                    {
                        colorMap[i] = regions[r].color;
                        break;
                    }
                }
                i++;
            }
        }
        MapDisplay display = FindObjectOfType<MapDisplay>();
        if (drawMode == DrawMode.NoiseMap)
        {
            display.DrawTexture(TextureGenerator.TextureFromHeightMap(noiseMap));
        }
        else if (drawMode == DrawMode.Mesh)
        {
            display.DrawMesh(MeshGenerator.GenerateMesh(noiseMap, heightMultiplier, heightCurve, detailLevel), TextureGenerator.TextureFromColorMap(colorMap, chunkSize, chunkSize));
        }
        else
        {
            display.DrawTexture(TextureGenerator.TextureFromColorMap(colorMap, chunkSize, chunkSize));
        }
    }

    void OnValidate()
    {
        if (lacunarity < 1)
        {
            lacunarity = 1;
        }
    }
}

[System.Serializable]
public struct TerrainType
{
    public float height;
    public Color color;
    public string name;
}