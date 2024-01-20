using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class GameCameraManager : MonoBehaviour
{
    [FormerlySerializedAs("_minCamMoveTime")]
    [Header("Camera settings")]
    [SerializeField] private float m_minCamMoveTime = 1f;
    [FormerlySerializedAs("_maxCamMoveTime")] [SerializeField] private float m_maxCamMoveTime = 2f;

    [FormerlySerializedAs("_enemyVCam")]
    [Header("Camera references")]
    [SerializeField] private CinemachineVirtualCamera m_enemyVCam;
    
    private Cinemachine3rdPersonFollow m_enemyVCamFollow;

    private bool m_targetCameraSide = true, m_currentCameraSide = false; // Left
    
    private void Start()
    {
        // Get the 3rd person follow component
        m_enemyVCamFollow = m_enemyVCam.GetCinemachineComponent<Cinemachine3rdPersonFollow>();
        
        // Set the target camera side
        m_targetCameraSide = !m_currentCameraSide;
        
        // Launch the camera move coroutine
        StartCoroutine(MoveCamera());
    }

    private IEnumerator MoveCamera()
    {
        while (true)
        {
            float moveStartTime = Time.time;
            float moveTime = Random.Range(m_minCamMoveTime, m_maxCamMoveTime);
        
            while (m_currentCameraSide != m_targetCameraSide)
            {
                float cameraSide = m_targetCameraSide ? Mathf.Lerp(0f, 1f, (Time.time - moveStartTime) / moveTime) : Mathf.Lerp(1f, 0f, (Time.time - moveStartTime) / moveTime);
                m_enemyVCamFollow.CameraSide = cameraSide;

                if (m_currentCameraSide)
                {
                    m_currentCameraSide = cameraSide != 0;
                }
                else
                {
                    m_currentCameraSide = cameraSide == 1 ? true : false;
                }
            
                yield return new WaitForEndOfFrame();
            }
            
            m_targetCameraSide = !m_targetCameraSide;

            yield return new WaitForEndOfFrame();
        }
    }
}
