using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    [SerializeField] private EnemyManager m_enemyManager;

    [SerializeField] private WeaponSO m_weaponSO;
    
    private IEnumerator Start()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
        
            m_enemyManager.DamageEnemy(m_weaponSO.m_weaponDamage);
        }
    }
}