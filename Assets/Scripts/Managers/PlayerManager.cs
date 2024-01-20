using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayerStats
{
    [SerializeField] private string m_playerName = "Anonymous";
    [SerializeField] private int m_playerLadder = 0;

    public string PlayerName => m_playerName;
    public int PlayerLadder => m_playerLadder;

    public void IncrementLadder(int value)
    {
        m_playerLadder += value;
    }
}

namespace Managers
{
    public class PlayerManager : MonoBehaviour
    {
        [SerializeField] private PlayerStats m_playerStats;

        public PlayerStats PlayerStats => m_playerStats;
    }
}