using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TD.Map;
using UnityEditor;

/// <summary>
/// Component that stores a TD Map and contains behavior to move the transform to a valid coordinate on that Map
/// </summary>
[ExecuteInEditMode]
public class SnapToMap : MonoBehaviour
{
    [SerializeField]
    Map targetMap;
    Vector3 lastPosition;

    bool invalidPosition = false;

    /// <summary>
    /// Moves this transform to the nearest valid coordinate on the TD Map
    /// </summary>
    public void SnapToGrid()
    {
        targetMap = FindObjectOfType<Map>();

        if(!targetMap.GridGenerated)
        {
            invalidPosition = true;
            Debug.LogWarning("Either no map is in the scene or the map needs to be regenerated");
            return;
        }

        if (transform.position != lastPosition)
        {
            Cell targetCell = targetMap.GetCellAtPosition(transform.position);
            Cell lastCell = targetMap.GetCellAtPosition(lastPosition);
            if (targetCell.IsOccupied && targetCell.occupant != gameObject)
            {
                invalidPosition = true;
                return;
            }

            invalidPosition = false;
            transform.position = targetMap.GetClosestCoordinatePosition(transform.position);
            lastPosition = transform.position;
            targetCell.occupant = gameObject;
            lastCell.occupant = null;
        }
        
    }

    private void OnDrawGizmos()
    {
        if(invalidPosition)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(transform.position, new Vector3(targetMap.CellSize, targetMap.CellSize, targetMap.CellSize));
        }
    }
}

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
