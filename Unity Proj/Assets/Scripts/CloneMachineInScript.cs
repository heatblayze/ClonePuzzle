using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class CloneMachineInScript : MonoBehaviour
{
    //Can the creation start?
    bool m_bCreationCanStart = false;

    //Whether the clone has been created
    bool m_bCloneCreated = false;
    //Whether the clone is now being created
    bool m_bCloneInCreation = false;

    //The linked OUT machine
    public GameObject m_gLinkedOutMachine = null;

    public List<GameObject> m_gSparkles;

	// Use this for initialization
	void Start ()
    {
        for (int i = 0; i < m_gSparkles.Count; i++)
        {
            m_gSparkles[i].gameObject.SetActive(false);
        }
	}

    // Update is called once per frame
    void Update()
    {
        if (m_bCreationCanStart && Input.GetAxis("Vertical") > 0 && !m_bCloneCreated)
        {
            m_gLinkedOutMachine.GetComponent<CloneMachineOutScript>().StartCreation();
            m_bCloneCreated = true;
        }
	}

    public void StartCreation()
    {
        //IT'S ALIVE!
    }

    public void SetAllowCreation(bool a_val)
    {
        m_bCreationCanStart = a_val;
    }
}
