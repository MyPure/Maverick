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
            int j = 0;
            for(int i = 0; i < platforms.Count; i++)
            {
                if (i > 0)
                {
                    if(platforms[i].transform.position.x == platforms[i - 1].transform.position.x)
                    {
                        platforms[i].order = platforms[i - 1].order;
                    }
                    else
                    {
                        platforms[i].order = platforms[i - 1].order + 1;
                    }
                }
                else
                {
                    platforms[i].order = 0;
                }
            }
        }
    }
}
