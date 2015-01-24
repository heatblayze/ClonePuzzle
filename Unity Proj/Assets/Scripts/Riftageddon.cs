using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class Riftageddon : MonoBehaviour
{
    //The rift prefab
    public GameObject m_gRiftPrefab;
    //Some nice text for you
    public GameObject m_wordToYourMother;
    SpriteRenderer[] m_renderers;

    //The amount of rifts to spawn
    int m_iRiftAmount = 14;
    //The list of rifts
    List<GameObject> m_lRifts;

    //Timer stuff
    float m_fMaxTime = 1f;
    float m_fTimer = 1f;

    //Should we start now?
    bool m_bStart = false;

    public Transform m_tTop;
    public Transform m_tLeft;
    public Transform m_tRight;
    public Transform m_tBottom;

	// Use this for initialization
	void Start ()
    {
        m_lRifts = new List<GameObject>();
        m_renderers = m_wordToYourMother.transform.GetComponentsInChildren<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (!m_bStart)
        {
            GameObject[] list = GameObject.FindObjectsOfType<GameObject>();
            for (int i = 0; i < list.Length; ++i)
            {
                if (list[i].name.Contains("Clone"))
                {
                    if (!list[i].name.Contains("Mac") && !list[i].name.Contains("Init") && !list[i].name.Contains("Station"))
                    {
                        m_bStart = true;
                        GameObject.Find("Player").GetComponent<PlayerContScript>().m_bCanControl = false;
                        break;
                    }
                }
            }
        }
        else
        {
            if (m_lRifts.Count < m_iRiftAmount)
            {
                //Spawn the rifts
                m_fTimer -= Time.deltaTime;
                if (m_fTimer < 0)
                {
                    if (m_lRifts.Count < 4)
                    {
                        m_renderers[3 - m_lRifts.Count].enabled = true;
                    }
                    m_fMaxTime -= 0.1f;
                    m_fTimer = m_fMaxTime;
                    float x = UnityEngine.Random.Range(m_tLeft.position.x, m_tRight.position.x);
                    float y = UnityEngine.Random.Range(m_tBottom.position.y, m_tTop.position.y);
                    m_lRifts.Add((GameObject)Instantiate(m_gRiftPrefab, new Vector3(x, y, 0), new Quaternion()));
                }
            }
        }
	}
}
