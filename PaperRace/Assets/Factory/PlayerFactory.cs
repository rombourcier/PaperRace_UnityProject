using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFactory : MonoBehaviour
{
    [SerializeField] PlayerManager m_PlayerManager = null;
    [SerializeField] int PlayerMax = 4;
    [SerializeField] List<Color> m_LPlayersColors = null;
    [SerializeField] Player m_PrefabPlayer = null;
    [SerializeField] Material m_PresetMat = null;
    
    // Start is called before the first frame update
   public void CreatePlayers(int nbPlayers)
   {
        if (!m_PlayerManager) return;
        for(int i = 0; i < nbPlayers && i < PlayerMax;i++)
        {
            m_PlayerManager.AddPlayer(Instantiate(m_PrefabPlayer));
            m_PlayerManager.GetPlayerById(i).SetColor(m_PresetMat, m_LPlayersColors[i]);
        }
   }
}
