using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof (TrackGenerator))]
public class TrackGeneratorEditor : Editor
{
    public override void OnInspectorGUI()
    {
        TrackGenerator  tg= (TrackGenerator)target;

        DrawDefaultInspector();

        if(GUILayout.Button("Generate"))
        {
            tg.GenerateTrack();
        }

        if (GUILayout.Button("Debug Log"))
        {
            tg.DebugLog();
        }
    }
}
