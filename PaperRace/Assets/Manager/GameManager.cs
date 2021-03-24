using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    static GameManager instance = null;
    public static GameManager Instance => instance;
    public int m_NbPlayers = 4;
    [SerializeField] PlayerFactory m_PlayerFactory = null;
    [SerializeField] PlayerManager m_PlayerManager = null;
    [SerializeField] BoardManager m_BoardManager = null;
    [SerializeField] UIHandler m_UIHandler = null;
    public UIHandler UIHandler => m_UIHandler;
    [SerializeField] Save m_Save = null;
    public Save Save => m_Save;
    bool GameOver = false;
    public void SetNbPlayers(int nbPlayers)
    {
        m_NbPlayers = nbPlayers;
    }
    private void Awake()
    {
        if (instance != null && instance != this)
            Destroy(gameObject);
        else
            instance = this;
    }

    public void PlayerEndTurn(int _Id)
    {
        m_PlayerManager.StartNextPlayerTurn(_Id);
        m_BoardManager.StartPlayerTurn(m_PlayerManager.GetNextPlayer(_Id));
        m_UIHandler.SetPlayerTurn(m_PlayerManager.GetCurrentPlayer().GetSpeed(),m_PlayerManager.GetCurrentPlayer().GetPlayerTurn());
    }
    public void PlayerCantMove()
    {
        m_PlayerManager.GetCurrentPlayer().Slow();
        PlayerEndTurn(m_PlayerManager.GetCurrentPlayer().GetId());
    }
    public void StartGame()
    {
        m_BoardManager.InitBoard();
        m_PlayerFactory.CreatePlayers(m_NbPlayers);
        m_PlayerManager.SetStartPos(m_BoardManager.GetPosStart());
        InitCam();
        m_PlayerManager.StartGame();
        m_BoardManager.StartPlayerTurn(m_PlayerManager.GetPlayerById(0));
        m_UIHandler.SetPlayerTurn(m_PlayerManager.GetCurrentPlayer().GetSpeed(), m_PlayerManager.GetCurrentPlayer().GetPlayerTurn());
    }
    void InitCam()
    {
        Camera.main.rect = m_BoardManager.GetRectBoard();
        Camera.main.orthographicSize = m_BoardManager.GetRectBoard().size.x / 2 + 3;
        Camera.main.transform.position = m_BoardManager.Center() + new Vector3(0, 10f, 0);
        Camera.main.transform.LookAt( m_BoardManager.Center());
    }
    public Player GetCurrentPlayer()
    {
        return m_PlayerManager.GetCurrentPlayer();
    }
    public void CellClicked(Cell cell)
    {
        if (GameOver)
        {

            m_UIHandler.EndGame();
            return;
        }
        m_PlayerManager.GetCurrentPlayer().MoveTo(cell);
        m_BoardManager.CellClicked(cell);
       // PlayerEndTurn(m_PlayerManager.GetCurrentPlayer().GetId());
    }
    public void PlayerFinish()
    {
        m_Save.SetSave(m_PlayerManager.GetCurrentPlayer().GetPlayerTurn(), "Map1");
        m_PlayerManager.CurrentEnd();
    }
    public void EndGame()
    {
        GameOver = true;
    }
}
