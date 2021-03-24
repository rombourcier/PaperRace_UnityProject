using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardManager : MonoBehaviour
{ 
    [SerializeField] int m_SizeBoard;
    [SerializeField] Board m_Board = null;
    [SerializeField] Vector2 m_StartPos = new Vector2(1,0);
    Vector2 m_CurrentPos = new Vector2();

    public void InitBoard()
    {
        m_StartPos = m_Board.GetStartPos();
        m_SizeBoard = m_Board.GetSize();
    }
    public void StartPlayerTurn(Player player, CellState cellState = CellState.clickable)
    {
        SetCellAround(player.GetCell().GetCellPos() + player.GetSpeed(), cellState);
    }
    void SetCellAround(Vector2 Pos,CellState cellState)
    {
        bool canMove = false;
        m_CurrentPos = Pos;
        Vector2 _PosPlayer = GameManager.Instance.GetCurrentPlayer().GetCell().GetCellPos();

        if (CanGoFromTo(_PosPlayer, Pos) || CellState.none == cellState)
        {
            canMove = true;
            m_Board.GetByPos(Pos)?.SetState(cellState);
        }

        if (CanGoFromTo(_PosPlayer, Pos + new Vector2(1, 1)) || CellState.none == cellState)
        {
            canMove = true;
            m_Board.GetByPos(Pos + new Vector2(1, 1))?.SetState(cellState);
        }

        if (CanGoFromTo(_PosPlayer, Pos + new Vector2(1, 0)) || CellState.none == cellState)
        {
            canMove = true;
            m_Board.GetByPos(Pos + new Vector2(1, 0))?.SetState(cellState);
        }

        if (CanGoFromTo(_PosPlayer, Pos + new Vector2(-1, 0)) || CellState.none == cellState)
        {
            canMove = true;
            m_Board.GetByPos(Pos + new Vector2(-1, 0))?.SetState(cellState);
        }
        if (CanGoFromTo(_PosPlayer, Pos + new Vector2(-1, -1)) || CellState.none == cellState)
        {
            canMove = true;
            m_Board.GetByPos(Pos + new Vector2(-1, -1))?.SetState(cellState);
        }
        if (CanGoFromTo(_PosPlayer, Pos + new Vector2(0, 1)) || CellState.none == cellState)
        {
            canMove = true;
            m_Board.GetByPos(Pos + new Vector2(0, 1))?.SetState(cellState);
        }

        if (CanGoFromTo(_PosPlayer, Pos + new Vector2(0, -1)) || CellState.none == cellState)
        {
            canMove = true;
            m_Board.GetByPos(Pos + new Vector2(0, -1))?.SetState(cellState);
        }

        if (CanGoFromTo(_PosPlayer, Pos + new Vector2(1, -1)) || CellState.none == cellState)
        {
            canMove = true;
            m_Board.GetByPos(Pos + new Vector2(1, -1))?.SetState(cellState);
        }

        if (CanGoFromTo(_PosPlayer, Pos + new Vector2(-1, 1)) || CellState.none == cellState)
        {
            canMove = true;
            m_Board.GetByPos(Pos + new Vector2(-1, 1))?.SetState(cellState);
        }
        if (!canMove)
            GameManager.Instance.PlayerCantMove();
    }
    bool CanGoFromTo(Vector2 _from, Vector2 _To)
    {
        Vector2 _current = _from;
        while(_current != _To)
        {
            _current = GetNextPos(_current, (_To - _current).normalized);
            if (!m_Board.GetByPos(_current) || m_Board.GetByPos(_current).GetSlowing())
                return false;
        }
        
        return true;
    }
    Vector2 GetNextPos(Vector2 _from, Vector2 _dir)
    {
        Vector2 _current = _from + new Vector2(.5f,.5f);
        while ((int)_current.x == (int)_from.x && (int)_current.y == (int)_from.y)
        {
            _current += (_dir.normalized * .1f);
        }
        _current.x = (int)_current.x;
        _current.y = (int)_current.y;
        return _current;
    }
    public Cell GetPosStart()
    {
        return m_Board.GetByPos(m_StartPos);
    }
    public void CellClicked(Cell cell)
    {
        SetCellAround(m_CurrentPos, CellState.none);
    }
    public Rect GetRectBoard()
    {
        return m_Board.GetRect();
    }
    public Vector3 Center()
    {
        return m_Board.MiddlePos();
    }
}
