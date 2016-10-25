using UnityEngine;
using System.Collections;

namespace LearningAnimals
{
    public class AI : MonoBehaviour
    {
        private int m_Joy = 0;
        public bool m_Grounded = false;

        [SerializeField]
        private int m_MoveSpeed = 700;
        [SerializeField]
        private int m_JumpForce = 50;

        private GameObject m_Target;

        [SerializeField]
        private LayerMask m_Layermask;

        private Rigidbody2D m_Rigidbody;

        void Start()
        {
            m_Target = GameObject.Find("Toy");
            m_Rigidbody = gameObject.GetComponent<Rigidbody2D>();
        }

        void Update()
        {
            if (Vector2.Distance(transform.position, m_Target.transform.position) > 2)
            {
                if (m_Target.transform.position.x > transform.position.x)
                {
                    m_Rigidbody.AddForce(new Vector2(1, 0) * m_MoveSpeed * Time.deltaTime, ForceMode2D.Force);
                }
                else if (m_Target.transform.position.x < transform.position.x)
                {
                    m_Rigidbody.AddForce(new Vector2(1, 0) * -m_MoveSpeed * Time.deltaTime, ForceMode2D.Force);
                }
                m_Joy++;
            }

            m_Grounded = Physics2D.OverlapCircle(transform.position, 2, m_Layermask);

            if(m_Joy >= 100 && m_Grounded)
            {
                m_Rigidbody.AddForce(new Vector2(0, 1) * m_JumpForce * Time.deltaTime, ForceMode2D.Impulse);
                m_Joy = 0;
            }
        }
    }
}