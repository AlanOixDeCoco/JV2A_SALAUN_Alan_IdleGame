using UI;
using UnityEngine;

namespace Managers
{
    public class GameUIManager : MonoBehaviour
    {
        [SerializeField] private FilledBar m_enemyHealthBar;
        [SerializeField] private FilledBar m_timerBar;

        public void UpdateEnemyHealthBar(float value)
        {
            m_enemyHealthBar.SetFillAmount(value);
        }
    }
}
