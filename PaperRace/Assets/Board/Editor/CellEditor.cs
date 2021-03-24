using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Cell)),CanEditMultipleObjects]
public class CellEditor : Editor
{

    Cell[] cell;
    bool IsSlowMat;
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        GUILayout.BeginHorizontal();
        IsSlowMat = (GUILayout.Toggle(IsSlowMat, "SlowingEffect"));
        if (GUILayout.Button("apply"))
        {
            for (int i = 0; i < cell.Length; i++)
                cell[i].SetSlowMat(IsSlowMat);
        }
        GUILayout.EndHorizontal();
        
    }
    private void OnEnable()
    {
        
        Object[] monoObject = targets;
        cell = new Cell[monoObject.Length];
        for (int i = 0; i < cell.Length; i++)
        {
            cell[i] = monoObject[i] as Cell;
        }
    }


}
