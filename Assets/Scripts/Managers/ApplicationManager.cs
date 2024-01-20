using UnityEngine;
using UnityEngine.Serialization;

namespace Managers
{
    public class ApplicationManager : MonoBehaviour
    {
        [FormerlySerializedAs("_targetFrameRate")]
        [Header("Application settings")]
        [Tooltip("Target frame rate, -1 is uncapped")][SerializeField] private int m_targetFrameRate = -1;
        
        private void Start()
        {
            Application.targetFrameRate = m_targetFrameRate;
        }
    }
}
