using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class UIHandler : MonoBehaviour
{
    [SerializeField] GameObject m_uiSpeed = null;
    [SerializeField] TMP_Text m_uiSpeedValue = null;
    [SerializeField] TMP_Text m_uiBestScore = null;
    [SerializeField] GameObject m_GameUi = null;
    [SerializeField] GameObject m_SelectionUI = null;
    [SerializeField] GameObject m_EndGameUI = null;

    [SerializeField] GameObject m_uiPlayerTurn = null;
    [SerializeField] TMP_Text m_uiPlayerTurnValue = null;

    public void SetPlayerTurn(Vector2 _speed, float nbTurn)
    {
        m_uiSpeedValue.text = _speed.ToString();
        m_uiPlayerTurnValue.text = nbTurn.ToString();
    }
    public void ShowGameUI(bool show)
    {
        m_GameUi.SetActive(show);
        m_SelectionUI.SetActive(!show);

    }
    public void EndGame()
    {
        m_GameUi.SetActive(false);
        m_SelectionUI.SetActive(false);
        m_EndGameUI.SetActive(true);
    }
    public void CloseGame()
    {
        Application.Quit();
    }
    public void SetBestScore()
    {
        m_uiBestScore.text = GameManager.Instance.Save.BestScore;
    }
    public void ReloadGame()
    {
        SceneManager.LoadScene(0);
    }
}
