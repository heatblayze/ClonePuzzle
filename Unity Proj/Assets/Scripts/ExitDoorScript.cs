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
    public float m_fCheckRad = 0.1f;
    //The layer for this check
    public LayerMask m_lPlayerLayer;

	// Use this for initialization
	void Start ()
    {
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
                //End Level
                Application.LoadLevel (Application.loadedLevel +1) ;
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
