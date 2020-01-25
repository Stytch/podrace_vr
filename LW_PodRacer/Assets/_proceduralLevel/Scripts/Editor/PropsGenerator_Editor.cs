using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(PropsGenerator))]
public class PropsGenerator_Editor : Editor
{
    public override void OnInspectorGUI()
    {
        PropsGenerator pg = (PropsGenerator)target;

        DrawDefaultInspector();

        if (GUILayout.Button("Generate Props"))
        {
            pg.GenerateAllProps();
        }
    }
}
