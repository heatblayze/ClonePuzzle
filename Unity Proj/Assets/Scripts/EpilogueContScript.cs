using UnityEngine;
using System.Collections;

public class EpilogueContScript : MonoBehaviour
{
    int m_switchNum = 0;
    int m_counter = 0;

    public Transform m_leftPoint;
    public Transform m_rightPoint;
    public Transform m_midPoint;
    public Transform m_riftPoint;

    float timer = 1f;

    public GameObject m_goldRiftPrefab;

    public GameObject m_quad;

	// Use this for initialization
	void Start ()
    {
        m_quad.renderer.material.color = new Color(1, 1, 1, 0);
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (GameObject.Find("Player"))
        {
            if (m_counter < 3)
            {
                switch (m_switchNum)
                {
                    case 0:
                        {
                            if (GameObject.Find("Player").GetComponent<PlayerContScript>().WalkToPoint(m_leftPoint.position))
                            {
                                m_switchNum = 1;
                                ++m_counter;
                            }
                            break;
                        }
                    case 1:
                        {
                            if (GameObject.Find("Player").GetComponent<PlayerContScript>().WalkToPoint(m_rightPoint.position))
                            {
                                m_switchNum = 0;
                                ++m_counter;
                            }
                            break;
                        }
                    default: break;
                }
            }
            else
            {
                if (GameObject.Find("Player").GetComponent<PlayerContScript>().WalkToPoint(m_midPoint.position))
                {
                    if (timer == 1f)
                    {
                        Instantiate(m_goldRiftPrefab, m_riftPoint.position, new Quaternion());
                    }

                    timer -= Time.deltaTime;
                    if (timer < 0)
                    {
                        GameObject.Find("Player").GetComponent<PlayerContScript>().WalkToPoint(m_riftPoint.position);
                    }
                }
            }
        }
        else
        {
            m_quad.renderer.material.color = new Color(1, 1, 1, m_quad.renderer.material.color.a + Time.deltaTime);
        }
	}
}
