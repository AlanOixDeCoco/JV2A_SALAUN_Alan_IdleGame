using System;
using System.Collections;
using System.Collections.Generic;
using ScriptableObjects.EventBridges;
using UnityEngine;
using UnityEngine.Serialization;

public class TestListener : MonoBehaviour
{
    [FormerlySerializedAs("_eventBridge")] [SerializeField] private EventBridge m_eventBridge;
    
    IEnumerator Start()
    {
        yield return new WaitForSeconds(2);
        
        m_eventBridge.Invoke();
    }
}
