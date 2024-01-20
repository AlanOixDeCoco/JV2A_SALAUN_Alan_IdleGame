using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

namespace Controllers
{
    public class EnemyController : MonoBehaviour
    {
        [Header("Enemy base references")]
        [SerializeField] private Transform m_bodyContainer;
        [SerializeField] private Transform m_headContainer;
        [SerializeField] private Transform m_leftArmContainer;
        [SerializeField] private Transform m_rightArmContainer;
        [SerializeField] private Transform m_leftHandContainer;
        [SerializeField] private Transform m_rightHandContainer;

        [Header("Other references")] 
        [SerializeField] private Animator m_animator;
        
        // Private variables
        private EnemyStats m_enemyStats;
        private GameManager m_gameManager;
        
        // Body parts
        private List<Transform> m_bodyParts = new List<Transform>();
        

        // Properties
        public Transform BodyContainer => m_bodyContainer;
        public Transform HeadContainer => m_headContainer;
        public Transform LeftArmContainer => m_leftArmContainer;
        public Transform RightArmContainer => m_rightArmContainer;
        public Transform LeftHandContainer => m_leftHandContainer;
        public Transform RightHandContainer => m_rightHandContainer;

        public void Setup(EnemyStats enemyStats, GameManager gameManager)
        {
            m_enemyStats = enemyStats;
            m_gameManager = gameManager;
            
            m_bodyParts.Add(Instantiate(enemyStats.m_enemyWeaponSO.m_weaponPrefab, m_rightHandContainer).transform);
        }
        
        public void SetupModel(GameObject bodyPart, GameObject headPart, GameObject leftArmPart, GameObject rightArmPart)
        {
            m_bodyParts.Add(Instantiate(bodyPart, m_bodyContainer).transform);
            m_bodyParts.Add(Instantiate(headPart, m_headContainer).transform);
            m_bodyParts.Add(Instantiate(leftArmPart, m_leftArmContainer).transform);
            m_bodyParts.Add(Instantiate(rightArmPart, m_rightArmContainer).transform);
            foreach (var part in m_bodyParts)
            {
                part.localPosition = Vector3.zero;
                part.localRotation = Quaternion.identity;
            }
            m_gameManager.GameUIManager.UpdateEnemyHealthBar(1);
        }

        public void TakeDamage(float amount)
        {
            // Take damage
            m_enemyStats.m_enemyHealth -= amount;

            if (m_enemyStats.m_enemyHealth <= 0)
            {
                m_enemyStats.m_enemyHealth = 0;
                Die();
            }

            m_gameManager.GameUIManager.UpdateEnemyHealthBar(m_enemyStats.m_enemyHealth /
                                                             m_enemyStats.m_enemyMaxHealth);
        }

        public void Die()
        {
            // Die
            m_gameManager.OnEnemyDie();

            m_animator.enabled = false;
            foreach (var part in m_bodyParts)
            {
                part.AddComponent<Rigidbody>();
            }
            
            // Spawn weapon using probability
            float random = Random.Range(0, 1f);
            if (random <= m_enemyStats.m_enemyWeaponSO.m_weaponDropProbability)
            {
                Debug.Log("Spawn weapon!");
            }
            
            Destroy(gameObject, 10f);
        }
    }
}
