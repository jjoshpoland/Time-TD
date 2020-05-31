#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(SnapToMap))]
public class SnapToMapEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        SnapToMap snapObject = (SnapToMap)target;

        if (GUILayout.Button("Snap To Map Grid"))
        {
            snapObject.SnapToGrid();
        }
    }


    private void OnSceneGUI()
    {
        //Get the current GUI event, see if it is a mouse up event, and apply the snap to grid behavior if it is
        SnapToMap snapObject = (SnapToMap)target;
        Event e = Event.current;
        int controlID = GUIUtility.GetControlID(FocusType.Passive);
        switch (e.GetTypeForControl(controlID))
        {

            case EventType.MouseUp:
                GUIUtility.hotControl = 0;
                e.Use();
                snapObject.SnapToGrid();

                //Debug.Log("up");
                break;

        }

    }
}
#endif