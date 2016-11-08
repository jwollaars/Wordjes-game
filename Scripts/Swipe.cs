using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Swipe : MonoBehaviour
{
    private Vector2 m_FirstPos;
    private Vector2 m_LastPos;

    private GameObject m_SelectedObj;

    [SerializeField]
    private LearningAnimals.AnimalController m_AnimalController;
    [SerializeField]
    private LearningAnimals.GameManager m_GameManager;

    [SerializeField]
    private int m_Force;

    void Update()
    {
        if (m_GameManager.GetGameState == LearningAnimals.GameState.Play)
        {
            if (!Application.isMobilePlatform)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    m_FirstPos = Input.mousePosition;
                    m_LastPos = Input.mousePosition;
                }

                if (Input.GetMouseButton(0))
                {
                    m_LastPos = Input.mousePosition;

                    Ray ray = Camera.main.ScreenPointToRay(m_LastPos);
                    RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);

                    if (hit.collider != null && hit.collider.gameObject.tag == "Throwable")
                    {
                        m_SelectedObj = hit.collider.gameObject;
                    }
                }

                if (Input.GetMouseButtonUp(0))
                {
                    if (m_SelectedObj == null)
                    {
                        m_AnimalController.RandomAnimal(Camera.main.ScreenToWorldPoint(Input.mousePosition));
                    }

                    if (m_SelectedObj != null && m_LastPos != m_FirstPos)
                    {
                        Vector2 dir = m_LastPos - m_FirstPos;
                        Vector2 normalDir = dir.normalized;

                        m_SelectedObj.GetComponent<Rigidbody2D>().AddForce(normalDir * m_Force * Time.deltaTime, ForceMode2D.Impulse);

                        m_SelectedObj = null;
                    }

                    if (m_SelectedObj != null && m_LastPos == m_FirstPos)
                    {
                        m_SelectedObj.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 1) * 4, ForceMode2D.Impulse);
                    }
                }
            }
            else if (Application.isMobilePlatform)
            {
                foreach (Touch touch in Input.touches)
                {
                    if (touch.phase == TouchPhase.Began)
                    {
                        m_FirstPos = touch.position;
                        m_LastPos = touch.position;
                    }

                    if (touch.phase == TouchPhase.Moved)
                    {
                        m_LastPos = touch.position;

                        Ray ray = Camera.main.ScreenPointToRay(m_LastPos);
                        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);

                        if (hit.collider != null && hit.collider.gameObject.tag == "Throwable")
                        {
                            m_SelectedObj = hit.collider.gameObject;
                        }
                    }

                    if (touch.phase == TouchPhase.Ended)
                    {
                        if (m_SelectedObj == null)
                        {
                            m_AnimalController.RandomAnimal(Camera.main.ScreenToWorldPoint(touch.position));
                        }

                        if (m_SelectedObj != null && m_LastPos != m_FirstPos)
                        {
                            Vector2 dir = m_LastPos - m_FirstPos;
                            Vector2 NormalDir = dir.normalized;

                            m_SelectedObj.GetComponent<Rigidbody2D>().AddForce(NormalDir * m_Force * Time.deltaTime, ForceMode2D.Impulse);

                            m_SelectedObj = null;
                        }
                    }
                }
            }
        }
    }
}