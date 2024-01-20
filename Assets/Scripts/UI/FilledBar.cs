using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace UI
{
    public class FilledBar : MonoBehaviour
    {
        [Header("Filled bar settings")]
        [SerializeField] private float m_oldFillUpdateDelay;
        [SerializeField] private float m_oldFillUpdateTime;

        [Header("Filled bar references")]
        [SerializeField] private Image m_fillImage;
        [SerializeField] private Image m_oldFillImage;
        
        private bool m_displayOldFill;
        private Coroutine m_oldBarFillUpdateCoroutine;

        private void Start()
        {
            // Decide if we use the old fill based on whether the image has been assigned or not
            m_displayOldFill = m_oldFillImage != null;
            
            // Set the current fill amount immediately (can use tween in the future)
            m_fillImage.fillAmount = 1;
        }

        public void SetFillAmount(float fillAmount)
        {
            // Set the current fill amount immediately (can use tween in the future)
            m_fillImage.fillAmount = fillAmount;
            
            // Stop here if there is no old bar
            if (!m_displayOldFill) return;
            
            // Stop the previous old bar fill coroutine if there is one
            //if (m_oldBarFillUpdateCoroutine != null) StopCoroutine(m_oldBarFillUpdateCoroutine);
            
            // Update the old bar using a coroutine
            m_oldBarFillUpdateCoroutine = StartCoroutine(UpdateOldBarFill());
        }

        private IEnumerator UpdateOldBarFill()
        {
            yield return new WaitForSeconds(m_oldFillUpdateDelay);

            float startFillAmount = m_oldFillImage.fillAmount;
            float updateStartTime = Time.time;

            while (Math.Abs(m_oldFillImage.fillAmount - m_fillImage.fillAmount) > 0.001f)
            {
                // Interpolate between current old fill amount --> target current fill amount
                float fillAmount = Mathf.Lerp(startFillAmount, m_fillImage.fillAmount,
                    (Time.time - updateStartTime) / m_oldFillUpdateTime);
                m_oldFillImage.fillAmount = fillAmount;

                yield return new WaitForEndOfFrame();
            }
            m_oldFillImage.fillAmount = m_fillImage.fillAmount;
        }
    }
}
