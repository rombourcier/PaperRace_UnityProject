using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Board))]
public class BoardEditor : Editor
{

    Board m_Board;
    Vector2 m_posStart = new Vector2();
    bool m_IsPosStart = true;
    Vector2 m_posEnd = new Vector2();
    bool m_IsPosEnd = true;


    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        m_Board = (Board)target;
        GUILayout.BeginHorizontal();
        if(GUILayout.Button("InitBoard"))
        {
            m_Board.InitBoard(m_Board.m_SizeBoard);
        }
        m_Board.m_SizeBoard = EditorGUILayout.IntField(m_Board.GetSize());

        GUILayout.EndHorizontal();
        SetStart();
        SetEnd();
    }

    void SetStart()
    {
        GUILayout.BeginHorizontal();
        m_Board.m_StartPos = EditorGUILayout.Vector2Field("Pos Start", m_Board.m_StartPos);

        GUILayout.EndHorizontal();
        m_IsPosStart = EditorGUILayout.Toggle("is Start", m_IsPosStart);

        if (GUILayout.Button("SetStartPos"))
        {
            m_Board.SetStartPos(m_Board.m_StartPos, m_IsPosStart);
        }
    }
    void SetEnd()
    {
        GUILayout.BeginHorizontal();
        m_posEnd = EditorGUILayout.Vector2Field("Pos End", m_posEnd);

        GUILayout.EndHorizontal();
        m_IsPosEnd = EditorGUILayout.Toggle("is End", m_IsPosEnd);

        if (GUILayout.Button("SetEndPos"))
        {
            m_Board.SetEndPos(m_posEnd, m_IsPosEnd);
        }
    }
}
