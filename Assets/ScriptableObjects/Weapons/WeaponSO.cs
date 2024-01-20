using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Weapon", fileName = "weapon_", order = 0)]
public class WeaponSO : ScriptableObject
{
    public string m_weaponName = "Weapon";
    public GameObject m_weaponPrefab;
    public Sprite m_weaponSprite;
    public float m_weaponDamage;
    public float m_weaponDropProbability = 0.1f;
}