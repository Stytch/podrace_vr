using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(MeshGenerator))]
public class MeshGeneratorEditor : Editor
{
    public override void OnInspectorGUI()
    {
        MeshGenerator mg = (MeshGenerator)target;

        DrawDefaultInspector();


        if (GUILayout.Button("Generate"))
        {
            mg.GenerateAllMesh();
        }
    }
}
