using UnityEngine;
using System.Collections;

namespace LearningAnimals
{
    public class HoldButton : MonoBehaviour
    {
        public bool m_IsHolding = false;

        private float m_Timer = 2f;

        [SerializeField]
        private UIManager m_UIManager;
        [SerializeField]
        private GameManager m_GameManager;

        void Update()
        {
            if (m_IsHolding)
            {
                m_Timer -= Time.deltaTime;
                if(m_Timer <= 0)
                {
                    m_GameManager.PauseGame();
                    m_UIManager.ShowMenu(1);
                    m_Timer = 2f;
                    m_IsHolding = false;
                }
            }
            else
            {
                m_Timer = 2f;
            }
        }

        public void SetBool(bool val)
        {
            m_IsHolding = val;
        }
    }
}