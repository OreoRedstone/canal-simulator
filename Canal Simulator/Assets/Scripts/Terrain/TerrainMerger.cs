using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainMerger : MonoBehaviour
{
    public TerrainData mainTerrain;
    public TerrainData canalTerrain;

    private void Start()
    {
        mainTerrain = GetComponent<TerrainGenerator>().GenerateTerrain(new TerrainData());
        GetComponent<Terrain>().terrainData = MergeTerrain(mainTerrain, canalTerrain);
        //GetComponent<Terrain>().terrainData = mainTerrain;
    }

    TerrainData MergeTerrain(TerrainData main, TerrainData canal)
    {
        TerrainData newData = new TerrainData();
        float[,] newHeightmap = new float[canal.heightmapResolution, canal.heightmapResolution];

        // Check over each point in the heightmaps.
        // Find the distance from the centre of the canal.
        // Lerp between the two heightmaps based on that value.

        for (int x = 0; x < canal.heightmapResolution; x++)
        {
            for (int y = 0; y < canal.heightmapResolution; y++)
            {
                float multiplier = canal.GetHeight(x, y) / 2;

                if(canal.GetHeight(x, y) == 0)
                {
                    newHeightmap[x, y] = 0;
                }
                else
                {
                    newHeightmap[x, y] = (main.GetHeight(x, y) / 5) - 0.5f;
                }

                //newHeightmap[x, y] = canal.GetHeight(x, y) + (main.GetHeight(x, y) * multiplier / 7);
                //Debug.Log(canal.GetHeight(x, y));
            }
        }

        newData.heightmapResolution = canal.heightmapResolution;
        newData.size = new Vector3(256, 20, 256);
        newData.SetHeights(0, 0, newHeightmap);

        return newData;
    }
}