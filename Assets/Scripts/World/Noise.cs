using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Noise
{
    public static float[,] GenerateNoiseMap(
        int width,
        int height,
        float scale,
        int octaves,
        float persistance,
        float lacunarity,
        int seed,
        Vector2 offset,
        bool isIsland,
        float landScale
    )
    {
        float[,] noiseMap = new float[width, height];

        if (scale <= 0)
        {
            scale = .0001f;
        }

        float maxTerrainHeight = float.NegativeInfinity;
        float minTerrainHeight = float.PositiveInfinity;

        System.Random prng = new System.Random(seed);
        Vector2[] octaveOffsets = new Vector2[octaves];

        for (int i = 0; i < octaves; i++)
        {
            float offsetX = prng.Next(-100000, 100000) + offset.x;
            float offsetY = prng.Next(-100000, 100000) + offset.y;
            octaveOffsets[i] = new Vector2(offsetX, offsetY);
        }

        float halfWidth = width / 2f;
        float halfHeight = height / 2f;
        
        // generate height y point for each point on map, with perlin noise
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                float frequency = 1.0f;
                float amplitude = 1.0f;
                float noiseHeight = 0.0f;

                for (int o = 0; o < octaves; o++)
                {

                    float sampleX = (x - halfWidth) / scale * frequency + octaveOffsets[o].x;
                    float sampleZ = (y - halfHeight) / scale * frequency + octaveOffsets[o].y;

                    float perlinValue = Mathf.PerlinNoise(sampleX, sampleZ) * 2 - 1;
                    noiseHeight += perlinValue * amplitude;

                    amplitude *= persistance;
                    frequency *= lacunarity;
                }
                if (noiseHeight > maxTerrainHeight)
                {
                    maxTerrainHeight = noiseHeight;
                }
                else if (noiseHeight < minTerrainHeight)
                {
                    minTerrainHeight = noiseHeight;
                }

                if (isIsland)
                {
                    Vector2 center = new Vector2(width * .5f, height * .5f);
                    float distance = Vector2.Distance(center, new Vector2(x, y));
                    float maxWidth = center.x * landScale;
                    float gradient = Mathf.Pow(distance / maxWidth, 2);
                    noiseHeight *= Mathf.Max(0, 1.0f - gradient * 2) - 1;
                }
                else
                {
                    Vector2 center = new Vector2(width * .5f, height * .5f);
                    float distance = Vector2.Distance(center, new Vector2(x, y));
                    float maxWidth = center.x * landScale;
                    float gradient = Mathf.Pow(distance / maxWidth, 2);
                    noiseHeight *= Mathf.Min(1.0f, 0.75f + gradient);
                }

                noiseMap[x, y] = noiseHeight;
            }
        }
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                noiseMap[x, y] = Mathf.InverseLerp(minTerrainHeight, maxTerrainHeight, noiseMap[x, y]);
            }
        }

        return noiseMap;
    }
}
