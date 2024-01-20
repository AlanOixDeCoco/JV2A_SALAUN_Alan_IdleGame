using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace ScriptableObjects.EventBridges
{
    [CreateAssetMenu(menuName = "Event Bridge", fileName = "eventBridge_", order = 0)]
    public class EventBridge : ScriptableObject
    {
        [FormerlySerializedAs("_event")] [HideInInspector]
        public UnityEvent m_event;
        
        public void Invoke()
        {
            m_event.Invoke();
        }
    }

    public class EventBridge<T> : ScriptableObject
    {
        [FormerlySerializedAs("_event")] [HideInInspector]
        public UnityEvent<T> m_event;

        public void Invoke(T argument)
        {
            m_event.Invoke(argument);
        }
    }
    
    [CreateAssetMenu(menuName = "Event Bridge (float)", fileName = "eventBridge_float_", order = 0)]
    public class FloatEventBridge : EventBridge<float> {}

    public class EventBridge<T1, T2> : ScriptableObject
    {
        [FormerlySerializedAs("_event")] [HideInInspector]
        public UnityEvent<T1, T2> m_event;

        public void Invoke(T1 argument1, T2 argument2)
        {
            m_event.Invoke(argument1, argument2);
        }
    }
}