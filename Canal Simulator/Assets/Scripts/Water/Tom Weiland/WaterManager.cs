using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class WaterManager : MonoBehaviour
{
    private MeshFilter meshFilter;
    private MeshRenderer meshRenderer;

    private void Awake()
    {
        meshFilter = GetComponent<MeshFilter>();
        meshRenderer = GetComponent<MeshRenderer>();
    }

    private void Update()
    {
        Vector3[] verticies = meshFilter.mesh.vertices;
        for (int i = 0; i < verticies.Length; i++)
        {
            verticies[i].y = WaveManager.instance.GetWaveHeight(transform.position.x + verticies[i].x * transform.localScale.x, transform.position.z + verticies[i].z * transform.localScale.z);
        }

        meshFilter.mesh.vertices = verticies;
        meshFilter.mesh.RecalculateNormals();

        /*Vector3[] verticies = meshFilter.mesh.vertices;
        for (int i = 0; i < verticies.Length; i++)
        {
            meshRenderer.material.SetFloat("_yHeight", WaveManager.instance.GetWaveHeight(transform.position.x + verticies[i].x, transform.position.z + verticies[i].z));
        }*/
    }
}
