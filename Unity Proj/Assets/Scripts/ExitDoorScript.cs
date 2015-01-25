using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class ExitDoorScript : MonoBehaviour
{
    //The values for the door being opened/closed
    List<bool> m_bOpen;
    //The list of objects that can alter the door
    public List<GameObject> m_gAlterers;

    //Transform used to check for the player being in front of the object
    public Transform m_tPlayerCheck;
    //The radius for this check
    float m_fCheckRad = 0.3f;
    //The layer for this check
    public LayerMask m_lPlayerLayer;

    //Timer for the end level scene thing
    const float m_fEndLevelMaxTime = 6.5f;
    float m_fEndLevelTimer = 0;
    //This tells us that it's playing
    bool m_bPlayEndScene = false;

	// Use this for initialization
	void Start ()
    {
        m_fEndLevelTimer = m_fEndLevelMaxTime;
        GetComponent<Animator>().speed = 0;
        m_bOpen = new List<bool>();
        if (m_gAlterers.Count > 0)
        {
            //Generate a list of bools for the door being closed/open based on the amount of alterers
            for (int i = 0; i < m_gAlterers.Count; ++i)
            {
                m_bOpen.Add(false);
            }
        }
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (m_bOpen.Count > 0)
        {
            //Check all bools to see if the door can be accessed
            for (int i = 0; i < m_bOpen.Count; ++i)
            {
                if (m_bOpen[i] == false)
                {
                    return;
                }
            }
        }

        //Check the button for going through door (and obviously only if the door is open)
        if (Input.GetAxis("Vertical") > 0)
        {
            //Make sure the player is in front of this object
            if (Physics.OverlapSphere(m_tPlayerCheck.position, m_fCheckRad, m_lPlayerLayer).Length > 0)
            {
                m_bPlayEndScene = true;
                audio.Play();
            }
        }
        if (m_bPlayEndScene)
        {
            m_fEndLevelTimer -= Time.deltaTime;
            GameObject.Find("Player").GetComponent<PlayerContScript>().m_bCanControl = false;
            if (m_fEndLevelTimer < 0)
            {
                //End Level
                Application.LoadLevel(Application.loadedLevel + 1);
            }
            else if (m_fEndLevelTimer < m_fEndLevelMaxTime && m_fEndLevelTimer > m_fEndLevelMaxTime / 1.75f)
            {
                GetComponent<Animator>().speed = 1f;
            }
            else if (m_fEndLevelTimer < m_fEndLevelMaxTime / 1.75f && m_fEndLevelTimer > 0)
            {
                GetComponent<Animator>().speed = -1f;
            }

            if (m_fEndLevelTimer < m_fEndLevelMaxTime / 1.3f)
            {
                GameObject.Find("Player").GetComponent<PlayerContScript>().EndLevel();
            }
        }
	}

    //Set whether the door is open
    public void SetActive(bool a_val, GameObject a_sender)
    {
        if (m_gAlterers.Count > 0)
        {
            for (int i = 0; i < m_gAlterers.Count; ++i)
            {
                //Make sure the sender was a registered alterer
                if (a_sender == m_gAlterers[i])
                {
                    m_bOpen[i] = a_val;
                    return;
                }
            }
        }
    }
}
