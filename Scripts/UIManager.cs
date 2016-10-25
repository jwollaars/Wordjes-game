using UnityEngine;
using UnityEngine.SceneManagement;

namespace LearningAnimals
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField]
        private GameObject[] m_Menus;

        [SerializeField]
        private GameObject[] m_HUD;
        public GameObject[] GetHUD
        {
            get
            {
                return m_HUD;
            }
        }

        public void ShowMenu(int index)
        {
            HideAllMenus();

            m_Menus[index].SetActive(true);
        }

        public void ShowAllMenus()
        {
            HideAllMenus();

            for (int i = 0; i < m_Menus.Length; i++)
            {
                m_Menus[i].SetActive(true);
            }
        }

        public void HideMenu(int index)
        {
            m_Menus[index].SetActive(false);
        }

        public void HideAllMenus()
        {
            for (int i = 0; i < m_Menus.Length; i++)
            {
                m_Menus[i].SetActive(false);
            }
        }

        public void ChangeScene(string scene)
        {
            SceneManager.LoadScene(scene);
        }

        public void Quit()
        {
            Application.Quit();
        }
    }
}