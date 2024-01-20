using System;
using System.Collections;
using System.Collections.Generic;
using Managers;
using UnityEngine;
using UnityEngine.Serialization;

public enum GameStates
{
    Spawning,
    Fighting,
    Pause,
}

public class GameManager : MonoBehaviour
{
    [Header("Game parameters")]
    [SerializeField] private int m_enemyKilledBonus = 20;
    [SerializeField] private int m_playerKilledMalus = -10;

    [Header("Managers")] 
    [SerializeField] private PlayerManager m_playerManager;
    [SerializeField] private EnemyManager m_enemyManager;
    [SerializeField] private GameUIManager m_gameUIManager;
    
    // Properties
    public int EnemyKilledBonus => m_enemyKilledBonus;
    public int PlayerKilledMalus => m_playerKilledMalus;
    public GameUIManager GameUIManager => m_gameUIManager;

    // Private variables
    private GameStates m_gameState = GameStates.Spawning;

    private void Start()
    {
        m_enemyManager.SpawnNewEnemy(m_playerManager.PlayerStats.PlayerLadder);

        m_gameState = GameStates.Fighting;
    }

    public void OnEnemyDie()
    {
        // Update player ladder
        m_playerManager.PlayerStats.IncrementLadder(m_enemyKilledBonus);
        
        // Spawn new enemy
        m_enemyManager.SpawnNewEnemy(m_playerManager.PlayerStats.PlayerLadder);
    }
}
