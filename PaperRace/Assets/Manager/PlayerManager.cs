using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    List<Player> m_dPlayers = new List<Player>();
    List<Player> m_Classement = new List<Player>();
    int m_nbPlayers;
    int m_CurrentPlayer;
    public Player GetCurrentPlayer() => GetPlayerById(m_CurrentPlayer);
    public void AddPlayer(Player player)
    {
        player.SetPlayerId(m_dPlayers.Count);
        m_dPlayers.Add(player);
    }
    public void StartNextPlayerTurn(int _Key)
    {
        GetCurrentPlayer().EndTurn();
        StartPlayerTurn(((_Key + 1) % m_dPlayers.Count));
    }
    public Vector2 GetSpeedPlayer(Player _Player)
    {
        return _Player.GetSpeed();
    }
    public Vector2 GetSpeedPlayer(int _Id)
    {
        return GetPlayerById(_Id).GetSpeed();
    }
    public Player GetPlayerById(int _Id)
    {
        return m_dPlayers[_Id];
    }
    public Player GetNextPlayer(int _Key)
    {
        return m_dPlayers[(_Key + 1) % m_dPlayers.Count];
    }
    public void StartGame()
    {
        StartPlayerTurn(0);
    }
    void StartPlayerTurn(int _Id)
    {
        m_CurrentPlayer = _Id;
        while (m_Classement.Contains(m_dPlayers[_Id]))
        {
            _Id++;
            _Id %= m_dPlayers.Count;
        }
        m_dPlayers[_Id].PlayTurn();

    }
    bool ClassementContain(int _id)
    {
        for (int i = 0; i < m_Classement.Count; i++)
            if (m_Classement[i].GetId() == _id)
                return true;
        return false;
    }
    public void SetStartPos(Cell _Cell)
    {
        for (int i = 0; i < m_dPlayers.Count; i++)
            m_dPlayers[i].SetStartPos(_Cell);
    }
    public void CurrentEnd()
    {
        m_Classement.Add(GetPlayerById(m_CurrentPlayer));
        if (m_Classement.Count == m_dPlayers.Count)
            GameManager.Instance.EndGame();
    }
}
