using UnityEngine;
using UnityEngine.UI;

namespace LearningAnimals
{
    public class AnimalController : MonoBehaviour
    {
        [SerializeField]
        private GameObject m_Animal;
        [SerializeField]
        private GameObject m_Toy;

        [SerializeField]
        private UIManager m_UIManager;

        private void Update()
        {

        }

        public void ChooseAnimal(Image image)
        {
            GameObject GO = new GameObject();
            GO.name = image.name.Substring(8);
            GO.transform.position = new Vector3(0f, 0f, 0f);
            GO.transform.rotation = Quaternion.identity;
            GO.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);

            GO.AddComponent<SpriteRenderer>();
            GO.GetComponent<SpriteRenderer>().sprite = image.sprite;

            GO.AddComponent<BoxCollider2D>();
            GO.AddComponent<Rigidbody2D>();
            GO.AddComponent<AI>();

            m_Animal = GO;

            m_UIManager.GetHUD[0].SetActive(false);
        }
    }
}