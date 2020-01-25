using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(MapGenerator))]
public class MapGeneratorEditor : Editor
{
    public int selected = 1;

    string[] options = new string[] { "12", "25", "50", "100" };
    int[] resolutionMap = new int[] {12,25,50,100 };
    float[] scaleFactor = new float[] { 0.72f, 0.18f, 0.045f, 0.01125f };

    public override void OnInspectorGUI()
    {
        MapGenerator mg = (MapGenerator)target;

        DrawDefaultInspector();

        selected = EditorGUILayout.Popup("Resolution Map", selected, options);
        mg.resolutionMap = resolutionMap[selected];
        mg.scaleFactor = scaleFactor[selected];

        if (GUILayout.Button("Generate"))
        {
            mg.Generate();
        }

        if (GUILayout.Button("Display HeighMap"))
        {
            mg.DisplayHeightMap();
        }

    }
}
