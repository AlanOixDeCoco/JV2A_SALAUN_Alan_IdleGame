using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Enemy Archetype", fileName = "enemyArchetype_", order = 0)]
public class EnemyArchetypeSO : ScriptableObject
{
    [Range(0, 1f)] public float m_damagePercentage;
    [Range(0, 1f)] public float m_resistancePercentage;
    [Range(0, 1f)] public float m_healPercentage;
    public float m_healthPercentage;

    public PrefabsSetSO m_bodyPrefabsSetSO;
    public PrefabsSetSO m_headPrefabsSetSO;
    public PrefabsSetSO m_armsPrefabsSetSO;
    
    public WeaponSO[] m_weaponOptions;
    
    private void OnValidate()
    {
        m_healthPercentage = 1f - (m_damagePercentage + m_resistancePercentage + m_healPercentage);
    }
}
