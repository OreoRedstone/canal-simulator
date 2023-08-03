using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreePlacer : MonoBehaviour
{
    public List<GameObject> trees = new List<GameObject>();
    public List<GameObject> grasses = new List<GameObject>();

    public int treeCount;
    public float treeMinHeight;

    public int grassesCount;
    public float grassMinHeight;

    public void Clear()
    {
        foreach (Transform child in transform)
        {
            if (child != transform)
            {
                if(child.gameObject != null)
                    DestroyImmediate(child.gameObject);
            }
        }
    }

    public void GenerateTrees()
    {
        foreach (Transform child in transform)
        {
            if (child != transform)
            {
                if (child.gameObject != null)
                    DestroyImmediate(child.gameObject);
            }
        }

        for (int i = 0; i < treeCount; i++)
        {
            GameObject tree = Instantiate(trees[UnityEngine.Random.Range(0, trees.Count - 1)]);
            tree.transform.parent = transform;
            tree.transform.localPosition = new Vector3(UnityEngine.Random.Range(0, 256), 0, UnityEngine.Random.Range(0, 512));
            float y = transform.GetComponent<Terrain>().SampleHeight(tree.transform.localPosition);
            Debug.Log(y);
            if(y > treeMinHeight)
            {
                tree.transform.localPosition = new Vector3(tree.transform.localPosition.x, y, tree.transform.localPosition.z);
            }
            else
            {
                DestroyImmediate(tree);
                i--;
            }
        }

        for (int i = 0; i < grassesCount; i++)
        {
            GameObject grass = Instantiate(grasses[UnityEngine.Random.Range(0, grasses.Count - 1)]);
            grass.transform.parent = transform;
            grass.transform.localPosition = new Vector3(UnityEngine.Random.Range(0, 256), 0, UnityEngine.Random.Range(0, 512));
            float y = transform.GetComponent<Terrain>().SampleHeight(grass.transform.localPosition);
            Debug.Log(y);
            if (y > grassMinHeight)
            {
                grass.transform.localPosition = new Vector3(grass.transform.localPosition.x, y, grass.transform.localPosition.z);
            }
            else
            {
                DestroyImmediate(grass);
                i--;
            }
        }
    }
}
