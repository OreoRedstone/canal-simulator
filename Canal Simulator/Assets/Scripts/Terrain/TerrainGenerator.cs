using System;
using UnityEngine;

[RequireComponent(typeof(Terrain))]
public class TerrainGenerator : MonoBehaviour
{
    public int width = 256; // X-axis of the terrain.
    public int height = 256; // Z-axis

    public int depth = 512; // Y-axis

    public float scale = 20f;
    public Vector2 seed;
    public float lacunarity;
    public float persistence;
    public int octaves;

    private void Start()
    {
        seed.x = UnityEngine.Random.Range(0f, 9999f);
        seed.y = UnityEngine.Random.Range(0f, 9999f);
        /*
        Terrain terrain = GetComponent<Terrain>();
        if (terrain.terrainData != null)
            terrain.terrainData = GenerateTerrain(terrain.terrainData);
        else
            terrain.terrainData= GenerateTerrain(new TerrainData());
        */
    }

    private void Update()
    {
        /*Terrain terrain = GetComponent<Terrain>();
        if (terrain.terrainData != null)
            terrain.terrainData = GenerateTerrain(terrain.terrainData);
        else
            terrain.terrainData= GenerateTerrain(new TerrainData());*/
    }

    // This generates a terrain data from a give heightmap.
    public TerrainData GenerateTerrain(TerrainData terrainData)
    {
        terrainData.heightmapResolution = width + 1;
        terrainData.size = new Vector3(width, depth, height);

        terrainData.SetHeights(0, 0, GenerateHeights(width, height, persistence, lacunarity, octaves, seed, scale));
        return terrainData;
    }

    // Generates a heightmap from the height function.
    float[,] GenerateHeights(int width, int height, float persistence, float lacunarity, int octaves, Vector2 seed, float scale)
    {
        float maxHeight = 0;
        float minHeight = 0;
        float[,] heights = new float[width, height];
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                heights[x, y] = CalculateHeight(x, y, persistence, lacunarity, octaves, seed, scale);
                if (heights[x, y] > maxHeight)
                    maxHeight = heights[x, y];
                else if (heights[x, y] < minHeight)
                    minHeight = heights[x, y];
            }
        }

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                heights[x,y] = Mathf.InverseLerp(minHeight, maxHeight, heights[x,y]);
            }
        }

        return heights;
    }

    // Generates the height at a given point.
    float CalculateHeight(int x, int y, float persistence, float lacunarity, int octaves, Vector2 seed, float scale)
    {
        float height = 0;
        float amplitude = 1;
        float frequency = 1;


        for (int i = 0; i < octaves; i++)
        {
            float sampleX = (float)x / scale * frequency + seed.x;
            float sampleY = (float)y / scale * frequency + seed.y;

            float perlinValue = Mathf.PerlinNoise(sampleX, sampleY);
            height += perlinValue * amplitude;

            amplitude *= persistence;
            frequency += lacunarity;
        }

        /*float xCoord = (float)x / width * scale + offsetX;
        float yCoord = (float)y / height * scale + offsetY;

        return Mathf.PerlinNoise(xCoord, yCoord);*/

        //Debug.Log(height);
        return height;
    }
}