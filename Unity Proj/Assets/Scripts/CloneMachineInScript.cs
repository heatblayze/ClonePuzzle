using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class CloneMachineInScript : MonoBehaviour
{
    //Is the machine turned on?
    public bool m_bCanUse = true;

    //Can the creation start?
    bool m_bCreationCanStart = false;

    //Whether the clone has been created
    bool m_bCloneCreated = false;
    //Whether the clone is now being created
    bool m_bCloneInCreation = false;

    //The linked OUT machine
    public List<GameObject> m_gLinkedOutMachines;

    //The sparkle emitters
    public List<GameObject> m_gSparkles;
    //The light emitters
    public List<Light> m_lLights;

	// Use this for initialization
	void Start ()
    {
        SetSparklesEnabled(false);
	}

    // Update is called once per frame
    void Update()
    {
        SetLightsEnabled(m_bCanUse && (!m_bCloneCreated) == true? true : false);
        //Make sure a clone has not been created and the machine is actually usable
        if (!m_bCloneCreated && m_bCanUse)
        {
            //Check that the player is inside and the button is pressed
            if (m_bCreationCanStart && Input.GetAxis("Vertical") > 0)
            {
                StartCreation();
            }
        }
	}

    void SetSparklesEnabled(bool a_val)
    {
        for (int i = 0; i < m_gSparkles.Count; i++)
        {
            m_gSparkles[i].GetComponent<ParticleEmitter>().emit = a_val;
        }
    }

    void SetLightsEnabled(bool a_val)
    {
        for (int i = 0; i < m_lLights.Count; i++)
        {
            m_lLights[i].enabled = a_val;
        }
    }

    public void StartCreation()
    {
        //IT'S ALIVE!
        SetSparklesEnabled(true);
        m_bCloneInCreation = true;
        for (int i = 0; i < m_gLinkedOutMachines.Count; ++i)
        {
            m_gLinkedOutMachines[i].GetComponent<CloneMachineOutScript>().StartCreation();
        }
    }

    public void SetAllowCreation(bool a_val)
    {
        //Used to check if the player is inside the machine and not jumping
        m_bCreationCanStart = a_val;
    }

    public void InterruptCreation()
    {
        //The cloning sequence was interrupted, stop it
        SetSparklesEnabled(false);
        m_bCloneInCreation = false;

        for (int i = 0; i < m_gLinkedOutMachines.Count; ++i)
        {
            m_gLinkedOutMachines[i].GetComponent<CloneMachineOutScript>().InterruptCreation();
        }
    }

    public void CreateSuccess()
    {
        //The clone has successfully been created
        m_bCloneCreated = true;
        SetSparklesEnabled(false);
    }

    public void SetActive(bool a_val)
    {
        m_bCanUse = a_val;
    }
}
