using UnityEngine;

namespace LearningAnimals
{
    public enum GameState { Play, Pause, End };

    public class GameManager : MonoBehaviour
    {
        private GameState m_GameState;
        public GameState GetGameState
        {
            get
            {
                return m_GameState;
            }
        }

        [SerializeField]
        private GameObject[] m_GameObjectsToStartOrPauseOrEnd;

        [SerializeField]
        private UIManager m_UIManager;

        private void Start()
        {
            m_GameState = GameState.Pause;
        }

        public void StartGame()
        {
            Time.timeScale = 1f;

            m_GameState = GameState.Play;

            for (int i = 0; i < m_GameObjectsToStartOrPauseOrEnd.Length; i++)
            {
                if (!m_GameObjectsToStartOrPauseOrEnd[i].activeSelf)
                {
                    m_GameObjectsToStartOrPauseOrEnd[i].SetActive(true);
                }
            }
        }

        public void PauseGame()
        {
            Time.timeScale = 0f;

            m_GameState = GameState.Pause;
        }

        public void EndGame()
        {
            Time.timeScale = 1f;

            m_GameState = GameState.End;

            for (int i = 0; i < m_GameObjectsToStartOrPauseOrEnd.Length; i++)
            {
                if (m_GameObjectsToStartOrPauseOrEnd[i].activeSelf)
                {
                    m_GameObjectsToStartOrPauseOrEnd[i].SetActive(false);
                }
            }
        }
    }
}