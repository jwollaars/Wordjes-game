using UnityEngine;
using System.Collections;

namespace LearningAnimals
{
    public class AI : MonoBehaviour
    {
        private LayerMask m_LayerMask;
        public LayerMask SetLayerMask
        {
            set
            {
                m_LayerMask = value;
            }
        }

        private int m_Joy = 0;
        public bool m_Grounded = false;

        [SerializeField]
        private int m_MoveSpeed = 10;
        [SerializeField]
        private int m_JumpForce = 2;

        private GameObject m_Target;

        private Rigidbody2D m_Rigidbody;

        private int m_Direction = 1;
        private int m_PreviousDirection = 1;

        private SpriteRenderer m_SpriteRenderer;

        private void Awake()
        {
            m_PreviousDirection = m_Direction;

            m_SpriteRenderer = GetComponent<SpriteRenderer>();
        }

        void Start()
        {
            m_Target = GameObject.Find("Toy");
            m_Rigidbody = gameObject.GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
            if (m_PreviousDirection != m_Direction)
            {
                m_PreviousDirection = m_Direction;

                if (m_SpriteRenderer != null)
                {
                    if (m_Direction == -1)
                    {
                        m_SpriteRenderer.flipX = false;
                    }
                    else if (m_Direction == 1)
                    {
                        m_SpriteRenderer.flipX = true;
                    }
                }
            }
        }

        void FixedUpdate()
        {
            if (Vector2.Distance(transform.position, m_Target.transform.position) > 2 && m_Grounded)
            {
                if (m_Target.transform.position.x > transform.position.x)
                {
                    m_Rigidbody.AddForce(new Vector2(1, 0) * m_MoveSpeed, ForceMode2D.Force);

                    if (m_Direction != 1)
                    {
                        m_Direction = 1;
                    }
                }
                else if (m_Target.transform.position.x < transform.position.x)
                {
                    m_Rigidbody.AddForce(new Vector2(1, 0) * -m_MoveSpeed, ForceMode2D.Force);

                    if (m_Direction != -1)
                    {
                        m_Direction = -1;
                    }
                }
                m_Joy++;
            }

            m_Grounded = Physics2D.OverlapCircle(transform.position, 2, m_LayerMask);

            if (m_Joy >= 100 && m_Grounded)
            {
                m_Rigidbody.AddForce(new Vector2(0, 1) * m_JumpForce, ForceMode2D.Impulse);
                m_Joy = 0;
            }
        }
    }
}