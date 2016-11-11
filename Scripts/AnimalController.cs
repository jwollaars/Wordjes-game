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

        //[SerializeField]
        //private GameObject m_Toy;
        [SerializeField]
        private PhysicsMaterial2D m_Bounce;

        [SerializeField]
        private Sprites m_Sprites = new Sprites();
        [SerializeField]
        private AnimalSounds m_AnimalSounds = new AnimalSounds();

        [SerializeField]
        private EventSystem m_EventSystem;

        public void ChooseAnimal(int index)
        {
            if (m_Sprites.availableSprites.Count > 0)
            {
                GameObject GO = new GameObject();
                GO.tag = "Throwable";
                GO.layer = LayerMask.NameToLayer("Animal");
                GO.name = m_Sprites.availableSprites[index].name.Substring(8);
                GO.transform.position = new Vector3(0f, 0f, 0f);
                GO.transform.rotation = Quaternion.identity;
                GO.transform.localScale = new Vector3(0.75f, 0.75f, 0.75f);

                GO.AddComponent<SpriteRenderer>();
                GO.GetComponent<SpriteRenderer>().sprite = m_Sprites.availableSprites[index];

                GO.AddComponent<BoxCollider2D>();
                GO.GetComponent<BoxCollider2D>().sharedMaterial = m_Bounce;

                GO.AddComponent<CircleCollider2D>();
                GO.GetComponent<CircleCollider2D>().isTrigger = true;
                GO.GetComponent<CircleCollider2D>().radius = 2;

                GO.AddComponent<Rigidbody2D>();
                GO.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;

                GO.AddComponent<AudioSource>();
                GO.GetComponent<AudioSource>().loop = false;

                GO.AddComponent<AI>();
                GO.GetComponent<AI>().SetLayerMask = m_LayerMask;
                GO.GetComponent<AI>().m_AudioClip = m_AnimalSounds.availableAnimalSounds[index];

                m_Animals.Add(GO);

                m_Sprites.unavailableSprites.Add(m_Sprites.availableSprites[index]);
                m_Sprites.availableSprites.Remove(m_Sprites.availableSprites[index]);

                m_AnimalSounds.unavailableAnimalSounds.Add(m_AnimalSounds.availableAnimalSounds[index]);
                m_AnimalSounds.availableAnimalSounds.Remove(m_AnimalSounds.availableAnimalSounds[index]);
            }
        }

        public void RandomAnimal(Vector2 pos)
        {
            if (!m_EventSystem.IsPointerOverGameObject())
            {
                if (m_Sprites.availableSprites.Count > 0)
                {
                    int randomAnimal = Random.Range(0, m_Sprites.availableSprites.Count);

                    GameObject GO = new GameObject();
                    GO.tag = "Throwable";
                    GO.layer = LayerMask.NameToLayer("Animal");
                    GO.name = m_Sprites.availableSprites[randomAnimal].name.Substring(8);
                    GO.transform.position = pos;
                    GO.transform.rotation = Quaternion.identity;
                    GO.transform.localScale = new Vector3(0.75f, 0.75f, 0.75f);

                    GO.AddComponent<SpriteRenderer>();
                    GO.GetComponent<SpriteRenderer>().sprite = m_Sprites.availableSprites[randomAnimal];

                    GO.AddComponent<BoxCollider2D>();
                    GO.GetComponent<BoxCollider2D>().sharedMaterial = m_Bounce;

                    GO.AddComponent<CircleCollider2D>();
                    GO.GetComponent<CircleCollider2D>().isTrigger = true;
                    GO.GetComponent<CircleCollider2D>().radius = 2;

                    GO.AddComponent<Rigidbody2D>();
                    GO.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;

                    GO.AddComponent<AudioSource>();
                    GO.GetComponent<AudioSource>().loop = false;

                    GO.AddComponent<AI>();
                    GO.GetComponent<AI>().SetLayerMask = m_LayerMask;
                    GO.GetComponent<AI>().m_AudioClip = m_AnimalSounds.availableAnimalSounds[randomAnimal];

                    m_Animals.Add(GO);
                    
                    m_Sprites.unavailableSprites.Add(m_Sprites.availableSprites[randomAnimal]);
                    m_Sprites.availableSprites.Remove(m_Sprites.availableSprites[randomAnimal]);

                    m_AnimalSounds.unavailableAnimalSounds.Add(m_AnimalSounds.availableAnimalSounds[randomAnimal]);
                    m_AnimalSounds.availableAnimalSounds.Remove(m_AnimalSounds.availableAnimalSounds[randomAnimal]);

                    if (m_Animals.Count > 6)
                    {
                        GameObject firstToDestroy = m_Animals[0];
                        m_Sprites.availableSprites.Add(m_Animals[0].GetComponent<SpriteRenderer>().sprite);
                        m_Sprites.unavailableSprites.Remove(m_Animals[0].GetComponent<SpriteRenderer>().sprite);
                        m_AnimalSounds.availableAnimalSounds.Add(m_Animals[0].GetComponent<AI>().m_AudioClip);
                        m_AnimalSounds.unavailableAnimalSounds.Remove(m_Animals[0].GetComponent<AI>().m_AudioClip);
                        m_Animals.Remove(m_Animals[0]);
                        Destroy(firstToDestroy);
                    }
                }
            }
        }

        [System.Serializable]
        private struct Sprites
        {
            public List<Sprite> availableSprites;
            public List<Sprite> unavailableSprites;
        }

        [System.Serializable]
        private struct AnimalSounds
        {
            public List<AudioClip> availableAnimalSounds;
            public List<AudioClip> unavailableAnimalSounds;
        }
    }
}