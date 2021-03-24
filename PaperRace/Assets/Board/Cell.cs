using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{

    [SerializeField] Material m_baseMat = null;
    [SerializeField] Material m_matClickable = null;
    [SerializeField] Material m_matClicked = null;
    [SerializeField] Material m_matStartPos = null;
    [SerializeField] Material m_matEndPos = null;
    [SerializeField] Material m_matSlowing = null;
    [SerializeField] Material m_EndPosClickable = null;

    [SerializeField]  bool m_IsEnd = false;
    public bool GetEnd() => m_IsEnd;
    bool m_Clickable;
    bool m_Clicked;
    [SerializeField]  bool m_IsSlowing = false;
    bool m_IsStart = false;
    public bool GetSlowing() => m_IsSlowing;
    [SerializeField]  Vector2 m_Pos;
    public Vector2 GetCellPos() => m_Pos;
    // Start is called before the first frame update

    public void InitCell(Vector2 Pos)
    {
        m_Pos = Pos;

        SetColor(m_baseMat);
    }
    public void SetSlowMat(bool slow)
    {
        m_IsSlowing = slow;
        if (slow)
            SetColor(m_matSlowing);
        else
            SetColor(m_baseMat);
        
    }

   public void SetStart(bool Start)
   {
       if(Start)
           SetColor(m_matStartPos);
       else
           SetColor(m_baseMat);

        m_IsStart = Start;
    }
    public void SetEnd(bool IsEnd)
    {

        m_IsEnd = IsEnd;
        if (IsEnd)
            SetColor(m_matEndPos);
        else
            SetColor(m_baseMat);

    }
    private void SetColor(Material mat)
    {
        GetComponent<MeshRenderer>().material = mat;
    }

    private void SetColorEditor(Material mat)
    {
        GetComponent<MeshRenderer>().material = mat;
    }
    public void OnMouseDown()
    {
        if (m_Clickable)
        {
            SetState(CellState.clicked);
        }
    }
    public void OnMouseUp()
    {
        if(m_Clicked)
        {
            GameManager.Instance.CellClicked(this);
        }
    }
    public void SetState(CellState state)
    {
        if (m_IsSlowing) return;
        switch (state)
        {
            case CellState.clickable:
               
                m_Clickable = true;
                m_Clicked = false;
                if (m_IsEnd)
                {
                    SetColor(m_EndPosClickable);
                    break;
                }
                SetColor(m_matClickable);
                break;
            case CellState.clicked:
                m_Clicked = true;
                m_Clickable = false;
                if (m_IsEnd)
                {
                    GameManager.Instance.PlayerFinish();
                    SetColor(m_matEndPos);
                    break;
                }

                SetColor(m_matClicked);
                
                break;
            case CellState.none:
                m_Clickable = false;
                m_Clicked = false;
                if(m_IsEnd)
                {
                    SetColor(m_matEndPos);
                    break;
                }
                SetColor(m_baseMat);
                break;
        }
    }
    
}
public enum CellState
{
    clicked,
    clickable,
    none
}