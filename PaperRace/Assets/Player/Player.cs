using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class Player : MonoBehaviour
{
    public event Action<bool>  TurnPlaying = null;
    public event Action<Material,Color>  PlayerInit = null;
    [SerializeField] LineRenderer m_trail = null;
    [SerializeField] string m_sName = "Bobby";
    [SerializeField] int m_PlayerId = 0;
    [SerializeField] Cell m_PLayerCell = null;
    [SerializeField] float m_OffsetLineRenderer = .8f;
    [SerializeField] int speed = 5;
    bool moving = false;
    public Material GetMat() => GetComponent<LineRenderer>().material;
    public Color GetColor() => m_color;
    Color m_color = Color.white ;


    int m_iTurnCount = 0;
    public int GetPlayerTurn() => m_iTurnCount;
    Vector2 Speed = new Vector2(0, 0);
    List<Vector3> m_playerPos = new List<Vector3>();
    private void Start()
    {
        TurnPlaying?.Invoke(false);
    }
    public void Slow()
    {
        Speed.x -= GetSign(Speed.normalized.x);
        Speed.y -= GetSign(Speed.normalized.y);
    }
    int GetSign(float i)
    {
        if (i > 0) return 1;
        if (i < 0) return -1;
        return 0;
    }
    public int GetId() => m_PlayerId;
    public Vector2 GetSpeed() => Speed;
    public Cell GetCell() => m_PLayerCell;
    public string GetName() => m_sName;
    public void SetPlayerId(int _Id) => m_PlayerId = _Id;
    public void SetColor(Material _source,Color _color)
    {
        PlayerInit?.Invoke(_source, _color);
        m_color = _color;
        m_trail.material = new Material(_source);
        m_trail.material.color = _color;
        m_trail.material.SetColor("_EmissionColor", _color);
    }
    public void PlayTurn()
    {
        TurnPlaying?.Invoke(true);
        m_iTurnCount++;
        m_trail.enabled = true;
    }
    public void Awake()
    {
        m_trail = gameObject.GetComponent<LineRenderer>();
        m_trail.positionCount = 0;
    } 
    public void MoveTo(Cell cell)
    {
        Speed = cell.GetCellPos() - m_PLayerCell.GetCellPos();
        m_PLayerCell = cell;
        moving = true;
    }
    public void SetStartPos(Cell cell)
    {
        transform.position = cell.transform.position + new Vector3(0,m_OffsetLineRenderer,0);
        m_trail.positionCount++;
        m_PLayerCell = cell;
        AddPosLineRenderer();
    }
    void AddPosLineRenderer()
    {
        if (!m_trail) return;
        m_playerPos.Add(transform.position);
        m_trail.positionCount = m_playerPos.Count;
        m_trail.SetPositions(m_playerPos.ToArray());
    }
    void Move()
    {
        transform.position += ((m_PLayerCell.transform.position + new Vector3(0, m_OffsetLineRenderer, 0)) - transform.position).normalized * speed * Time.deltaTime;
        AddPosLineRenderer();
        if ((transform.position - (m_PLayerCell.transform.position + new Vector3(0, m_OffsetLineRenderer, 0))).magnitude < .1f)
        {
            transform.position = m_PLayerCell.transform.position + new Vector3(0, m_OffsetLineRenderer, 0);
            AddPosLineRenderer();
            moving = false;
            GameManager.Instance.PlayerEndTurn(m_PlayerId);
        }
    }
    public void EndTurn()
    {
        m_trail.enabled = false;
        TurnPlaying?.Invoke(false);
    }
    private void Update()
    {
        if(moving)
            Move();
    }
}
