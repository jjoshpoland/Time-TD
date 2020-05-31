using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using TD.Maps;

[CustomEditor(typeof(Map))]
public class MapEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        Map map = (Map)target;

        if (GUILayout.Button("Generate"))
        {
            map.GenerateGridInEditor();
        }
        if (GUILayout.Button("Destroy"))
        {
            map.DestroyGridInEditor();
        }
    }
}
