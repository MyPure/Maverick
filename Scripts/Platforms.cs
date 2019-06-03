using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Platforms : MonoBehaviour
{

}

[CanEditMultipleObjects]
[CustomEditor(typeof(Platforms))]
public class PlatformsEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        if (GUILayout.Button("SortPlatforms"))
        {
            List<Platform> platforms = new List<Platform>(((Platforms)target).GetComponentsInChildren<Platform>());
            platforms.Sort();
            for(int i = 0; i < platforms.Count; i++)
            {
                platforms[i].order = i;
            }
        }
    }
}
