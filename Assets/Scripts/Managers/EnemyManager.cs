using System;
using System.Collections;
using System.Collections.Generic;
using Controllers;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyStats
{
    public string m_enemyName = "Skeleton";
    public float m_enemyMaxHealth;
    public float m_enemyHealth;
    public float m_enemyDamage;
    public float m_enemyResistance;
    public float m_enemyHeal;
    public WeaponSO m_enemyWeaponSO;

    // Constructor
    public EnemyStats(EnemyArchetypeSO enemyArchetypeSO, int enemyDifficulty)
    {
        // Create stats based on the enemy difficulty & archetype
        m_enemyMaxHealth = enemyDifficulty * enemyArchetypeSO.m_healthPercentage;
        m_enemyHealth = m_enemyMaxHealth;
        m_enemyDamage = enemyDifficulty * enemyArchetypeSO.m_damagePercentage;
        m_enemyResistance = enemyDifficulty * enemyArchetypeSO.m_resistancePercentage;
        m_enemyHeal = enemyDifficulty * enemyArchetypeSO.m_healPercentage;

        int random = Random.Range(0, enemyArchetypeSO.m_weaponOptions.Length);
        m_enemyWeaponSO = enemyArchetypeSO.m_weaponOptions[random];
    }
}

public class EnemyManager : MonoBehaviour
{
    [Header("Managers references")]
    [SerializeField] private GameManager m_gameManager;

    [Header("Enemies generation")]
    [SerializeField] private EnemyArchetypeSO[] m_enemyArchetypeSos;
    [SerializeField] private GameObject m_enemyBase;

    [Header("Architecture references")] 
    [SerializeField] private Transform m_enemiesParent;

    [SerializeField] private Transform m_vCamTarget;
    
    public GameObject EnemyBase => m_enemyBase;
    
    // Private variables
    private EnemyController m_currentEnemyController;

    public void SpawnNewEnemy(int enemyDifficulty)
    {
        int random = Random.Range(0, m_enemyArchetypeSos.Length);
        var newEnemyArchetype = m_enemyArchetypeSos[random];
        var newEnemyStats = new EnemyStats(newEnemyArchetype, enemyDifficulty);

        // Remove previous enemy
        //if (m_currentEnemyController != null) Destroy(m_currentEnemyController.gameObject);
        
        var newEnemy = Instantiate(m_enemyBase, m_enemiesParent);
        m_currentEnemyController = newEnemy.GetComponent<EnemyController>();
        m_currentEnemyController.Setup(newEnemyStats, m_gameManager);
        
        // Generate random model
        var randomBody = newEnemyArchetype.m_bodyPrefabsSetSO.GetRandomPrefab();
        var randomHead = newEnemyArchetype.m_headPrefabsSetSO.GetRandomPrefab();
        var randomLeftArm = newEnemyArchetype.m_armsPrefabsSetSO.GetRandomPrefab();
        var randomRightArm = newEnemyArchetype.m_armsPrefabsSetSO.GetRandomPrefab();
        
        m_currentEnemyController.SetupModel(randomBody, randomHead, randomLeftArm, randomRightArm);
        
        m_vCamTarget.position = m_currentEnemyController.HeadContainer.position;
    }

    public void DamageEnemy(float amount)
    {
        if(m_currentEnemyController == null) return;
        m_currentEnemyController.TakeDamage(amount);
    }
}
