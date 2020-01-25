using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(TrackManager))]
public class TrackManagerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        TrackManager tm = (TrackManager)target;

        DrawDefaultInspector();

        if (GUILayout.Button("Generate Track"))
        {
            tm.GenerateTrack();
        }

        if (GUILayout.Button("Clean All"))
        {
            tm.CleanAll();
        }
    }
}
