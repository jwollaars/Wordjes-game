using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections.Generic;

namespace LearningAnimals
{
    public class AnimalController : MonoBehaviour
    {
        [SerializeField]
        private Camera m_Camera;
        [SerializeField]
        private LayerMask m_LayerMask;

        private List<GameObject> m_Animals = new List<GameObject>();

        [SerializeField]
        private GameObject m_Toy;
        [SerializeField]
        private PhysicsMaterial2D m_Bounce;
        [SerializeField]
        private List<Image> m_Images;

        [SerializeField]
        private EventSystem m_EventSystem;

        public void ChooseAnimal(Image image)
        {
            if (m_Images.Count > 0)
            {
                GameObject GO = new GameObject();
                GO.tag = "Throwable";
                GO.layer = LayerMask.NameToLayer("Animal");
                GO.name = image.name.Substring(8);
                GO.transform.position = new Vector3(0f, 0f, 0f);
                GO.transform.rotation = Quaternion.identity;
                GO.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);

                GO.AddComponent<SpriteRenderer>();
                GO.GetComponent<SpriteRenderer>().sprite = image.sprite;

                GO.AddComponent<BoxCollider2D>();
                GO.GetComponent<BoxCollider2D>().sharedMaterial = m_Bounce;

                GO.AddComponent<Rigidbody2D>();
                GO.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;

                GO.AddComponent<AI>();
                GO.GetComponent<AI>().SetLayerMask = m_LayerMask;

                m_Animals.Add(GO);
                m_Images.Remove(image);
            }
        }

        public void RandomAnimal(Vector2 pos)
        {
            if (!m_EventSystem.IsPointerOverGameObject())
            {
                if (m_Images.Count > 0)
                {
                    int randomAnimal = Random.Range(0, m_Images.Count);

                    GameObject GO = new GameObject();
                    GO.tag = "Throwable";
                    GO.layer = LayerMask.NameToLayer("Animal");
                    GO.name = m_Images[randomAnimal].name.Substring(8);
                    GO.transform.position = pos;
                    GO.transform.rotation = Quaternion.identity;
                    GO.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);

                    GO.AddComponent<SpriteRenderer>();
                    GO.GetComponent<SpriteRenderer>().sprite = m_Images[randomAnimal].sprite;

                    GO.AddComponent<BoxCollider2D>();
                    GO.GetComponent<BoxCollider2D>().sharedMaterial = m_Bounce;

                    GO.AddComponent<Rigidbody2D>();
                    GO.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;

                    GO.AddComponent<AI>();
                    GO.GetComponent<AI>().SetLayerMask = m_LayerMask;

                    m_Animals.Add(GO);
                    m_Images.Remove(m_Images[randomAnimal]);
                }
            }
        }
    }
}