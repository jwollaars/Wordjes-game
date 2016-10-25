using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace LearningAnimals
{
    public class AnimalController : MonoBehaviour
    {
        private GameObject m_Animal;
        private Vector3 m_Position = new Vector3(0f, 0f, 0f);

        private SpriteRenderer m_SR;

        [SerializeField]
        private UIManager m_UIManager;

        public void ChooseAnimal(GameObject GO)
        {
            m_Animal = new GameObject();
            m_Animal.AddComponent<Transform>();
            m_SR = m_Animal.AddComponent<SpriteRenderer>();
            m_SR.sprite = GO.GetComponent<Image>().sprite;

            GameObject spawnedAnimal = (GameObject)Instantiate(m_Animal, m_Position, Quaternion.identity);
            spawnedAnimal.AddComponent<BoxCollider2D>();
            spawnedAnimal.AddComponent<Rigidbody2D>();
            spawnedAnimal.AddComponent<AI>();

            m_UIManager.GetHUD[0].SetActive(false);
        }
    }
}