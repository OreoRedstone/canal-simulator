using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(TreePlacer))]
public class CustomButtons : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        TreePlacer myScript = (TreePlacer)target;
        if (GUILayout.Button("Generate"))
        {
            myScript.GenerateTrees();
        }
        if (GUILayout.Button("Clear"))
        {
            myScript.Clear();
        }
    }
}
