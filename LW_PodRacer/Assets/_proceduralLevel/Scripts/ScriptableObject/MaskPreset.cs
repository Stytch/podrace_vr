using UnityEngine;

[CreateAssetMenu(fileName = "PresetMask", menuName = "ScriptableObjects/PresetMask", order = 1)]
public class PresetMask : ScriptableObject
{
    public Vector2[] Mask;
}