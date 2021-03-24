using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    public int m_SizeBoard;
    [SerializeField] List<Cell> m_lCellPlayer = new List<Cell>();
    [SerializeField] Cell m_PrefabCell  = null;
    public Vector2 m_StartPos = new Vector2(0,0);
    Vector2 m_endPos = new Vector2(0,0);
    public Vector2 GetStartPos() => m_StartPos;
    public int GetSize() => m_SizeBoard;
    public void InitBoard(int SizeBoard)
    {
        FreeBoard();
        m_SizeBoard = SizeBoard;
        for (int i = 0; i < m_SizeBoard * m_SizeBoard; i++)
        {
            m_lCellPlayer.Add(Instantiate(m_PrefabCell));
            m_lCellPlayer[i].transform.position = new Vector3(i % m_SizeBoard,0, i / m_SizeBoard);
            m_lCellPlayer[i].InitCell(new Vector2(i % m_SizeBoard, i / m_SizeBoard));
        }
    }
    public void InitBoard(int SizeBoard,List<int> IndexSlowMats)
    {
        FreeBoard();
        m_SizeBoard = SizeBoard;
        for (int i = 0; i < m_SizeBoard * m_SizeBoard; i++)
        {
            m_lCellPlayer.Add(Instantiate(m_PrefabCell));
            m_lCellPlayer[i].transform.position = new Vector3(i % m_SizeBoard, 0, i / m_SizeBoard);
            m_lCellPlayer[i].InitCell(new Vector2(i % m_SizeBoard, i / m_SizeBoard));
            if (IndexSlowMats.Contains(i))
                m_lCellPlayer[i].SetSlowMat(true);
        }
    }
    public void SetStartPos(Vector2 StartPos,bool IsStart)
    {
        if (GetByPos(m_StartPos)) GetByPos(m_StartPos).SetStart(false);
        m_StartPos = StartPos;
        GetByPos(StartPos).SetStart(IsStart);
    }
    public void SetEndPos(Vector2 EndPos,bool IsEnd = true)
    {
        Debug.Log(EndPos);
        if (GetByPos(m_endPos)) GetByPos(m_endPos).SetEnd(false);
        m_endPos = EndPos;
        GetByPos(EndPos).SetEnd(IsEnd);
    }
    public void FreeBoard()
    {
        for (int i = 0; i < m_lCellPlayer.Count; i++)
            DestroyImmediate(m_lCellPlayer[i].gameObject);
        m_lCellPlayer.Clear();
    }
    public Cell GetByIndex(int id)
    {
        return m_lCellPlayer[id % m_SizeBoard + id / m_SizeBoard * m_SizeBoard];
    }
    public Cell GetByPos(Vector2 Pos)
    {
        ClampPos(ref Pos);
        if (Pos.x >= m_SizeBoard || Pos.y >= m_SizeBoard) return null;                                                      
        return m_lCellPlayer[(int)(Pos.x + Pos.y * m_SizeBoard)];
    }
    void ClampPos(ref Vector2 Pos)
    {
        if (Pos.x < 0) Pos.x = m_SizeBoard;
        if (Pos.y < 0) Pos.y = m_SizeBoard;
        if (Pos.y > m_SizeBoard - 1) Pos.y = m_SizeBoard;
        if (Pos.x > m_SizeBoard - 1) Pos.x = m_SizeBoard;
    }
    public Rect GetRect()
    {
        return new Rect(0, 0, m_SizeBoard, m_SizeBoard);
    }
    public Vector3 MiddlePos()
    {
        Vector3 center = new Vector3();
        for (int i = 0; i < m_SizeBoard * m_SizeBoard; i++)
            center += m_lCellPlayer[i].transform.position / (m_SizeBoard * m_SizeBoard);
        return center;
    }
    /*
    private void Awake()
    {
        int a = Func(2); Debug.Log(a);
    }
    int Func(int a)
    {
        Debug.Log(a);
        return a;
    }*/
}
