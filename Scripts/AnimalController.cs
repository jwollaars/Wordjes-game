using UnityEngine;
using System.Collections;

namespace LearningAnimals
{
    public class AnimalController : MonoBehaviour
    {
        private GameObject m_Animal;
        private Vector3 m_Position = new Vector3(0f, 0f, 0f);

        [SerializeField]
        private UIManager m_UIManager;

        public void ChooseAnimal(GameObject GO)
        {
            m_Animal = GO;

            GameObject spawnedAnimal = (GameObject)Instantiate(m_Animal, m_Position, Quaternion.identity);

            m_UIManager.GetHUD[0].SetActive(false);
        }
    }
}