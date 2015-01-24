using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class PressurePadScript : MonoBehaviour
{
    //The current state of the pressure pad
    public bool m_bCanUse = true;
    //The current state of the pressure pad's signal
    bool m_bValue = false;
    //Used to determine whether the signal will always remain active once triggered
    public bool m_bConstantSignal = false;

    //The object(s) the pressure pad affects
    public List<GameObject> m_lAffectedObjects = null;

    //The layer used to detect players
    public int m_iPlayerLayer;

	// Use this for initialization
	void Start ()
    {

	}
	
	// Update is called once per frame
	void Update ()
    {
	
	}

    void OnCollisionEnter(Collision a_collision)
    {
        //Make sure the object is usable
        if (m_bCanUse)
        {
            //Is the collided object a player?
            if (a_collision.gameObject.layer == m_iPlayerLayer)
            {
                //Change the value and send it
                m_bValue = true;
                SendSignal();
            }
        }
    }

    void OnCollisionExit(Collision a_collision)
    {
        //Make sure the object is usable and NOT a constant signal
        if (!m_bConstantSignal && m_bCanUse)
        {
            //Is the collided object a player?
            if (a_collision.gameObject.layer == m_iPlayerLayer)
            {
                //Change the value and send it
                m_bValue = false;
                SendSignal();
            }
        }
    }

    void SendSignal()
    {
        //If this is empty then you forgot to add objects to it!
        if (m_lAffectedObjects.Count == 0)
        {
            //Yeah, you tell 'em
            Debug.Log("I'm so hungry! My list is empty!");
            //Leave this function, there's nothing more we can do
            return;
        }

        //In case we're still having trouble or devving away
        Debug.Log("Sending signal!" + m_bValue);

        //Loop through all objects that get a signal sent to them and send the signal
        //Just making sure to check that the object contains the script you're trying to access
        for (int i = 0; i < m_lAffectedObjects.Count; i++)
        {
            if (m_lAffectedObjects[i].GetComponent<PressurePadScript>() != null)
            {
                m_lAffectedObjects[i].GetComponent<PressurePadScript>().SetActive(m_bValue);
                continue;
            }

            if (m_lAffectedObjects[i].GetComponent<ExitDoorScript>() != null)
            {
                m_lAffectedObjects[i].GetComponent<ExitDoorScript>().SetActive(m_bValue);
                continue;
            }

            //Do more for the rest of the object types
        }
    }

    public void SetActive(bool a_val)
    {
        //Changes the usability of this object
        m_bCanUse = a_val;
    }
}
