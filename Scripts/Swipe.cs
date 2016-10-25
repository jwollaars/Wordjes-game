using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Swipe : MonoBehaviour
{
    private Vector2 m_FirstPos;
    private Vector2 m_LastPos;

    private GameObject m_SelectedObj;

    [SerializeField]
    private int m_Force;

    void Update()
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
                Vector2 dir = m_LastPos - m_FirstPos;
                Vector2 NormalDir = dir.normalized;

                m_SelectedObj.GetComponent<Rigidbody2D>().AddForce(NormalDir * m_Force * Time.deltaTime, ForceMode2D.Impulse);

                m_SelectedObj = null;
            }
        }
    }
}