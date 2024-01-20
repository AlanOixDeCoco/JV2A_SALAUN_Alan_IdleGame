using System;
using System.Collections;
using System.Collections.Generic;
using ScriptableObjects.EventBridges;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public class EventBridgeListener : MonoBehaviour
{
    [FormerlySerializedAs("_eventBridge")] [SerializeField] private EventBridge m_eventBridge;
    [FormerlySerializedAs("_onEventRaised")] [SerializeField] private UnityEvent m_onEventRaised;

    private void OnEnable()
    {
        m_eventBridge.m_event.AddListener(m_onEventRaised.Invoke);
    }
    
    private void OnDisable()
    {
        m_eventBridge.m_event.RemoveListener(m_onEventRaised.Invoke);
    }
}