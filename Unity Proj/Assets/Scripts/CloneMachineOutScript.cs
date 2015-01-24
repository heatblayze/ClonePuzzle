using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class CloneMachineOutScript : MonoBehaviour
{
    //Whether the clone has been created
    bool m_bCloneCreated = false;
    //Whether the clone is now being created
    bool m_bCloneInCreation = false;

    //The linked IN machine
    public GameObject m_gLinkedInMachine = null;

    //The prefab of the clone/player
    public GameObject m_gClonePrefab = null;
    //The position the clone will spawn at
    public Transform m_tCloneInitPos;

    //The sparkle emitters
    public List<GameObject> m_gSparkles;
    //The light emitters
    public List<Light> m_lLights;

    //The timer for the creation of a clone
    const float m_fCreationMaxTime = 2f;
    float m_fCreationTimer = 2f;

    // Use this for initialization
    void Start()
    {
        SetSparklesEnabled(false);
	}
	
	// Update is called once per frame
	void Update ()
    {
        //Run the creation animation and create the clone when it returns true
        if (CreationAnim())
        {
            if (!m_bCloneCreated)
            {
                //Generate the clone
                Instantiate(m_gClonePrefab, m_tCloneInitPos.position, Quaternion.Euler(new Vector3(0, 90, 0)));
                //Turn off the sparkles, can't make another clone
                m_bCloneCreated = true;
                SetSparklesEnabled(false);
                //Tell the first machine this info too
                m_gLinkedInMachine.gameObject.GetComponent<CloneMachineInScript>().CreateSuccess();
            }
        }
	}

    void SetSparklesEnabled(bool a_val)
    {
        for (int i = 0; i < m_gSparkles.Count; i++)
        {
            m_gSparkles[i].gameObject.GetComponent<ParticleEmitter>().emit = a_val;
        }
    }

    public void SetLightsEnabled(bool a_val)
    {
        for (int i = 0; i < m_lLights.Count; i++)
        {
            m_lLights[i].enabled = a_val;
        }
    }

    public void StartCreation()
    {
        GetComponent<AudioSource>().Play();
        //IT'S ALIVE!
        m_bCloneInCreation = true;
        return;
    }

    bool CreationAnim()
    {
        //Make sure the cloning has begun
        if (m_bCloneInCreation)
        {
            //Countdown
            m_fCreationTimer = m_fCreationTimer - Time.deltaTime;

            //Creation in progress
            if (m_fCreationTimer > 0)
            {
                SetSparklesEnabled(true);
                return false;
            }
            else
            {
                //Time's up! Create the player
                return true;
            }
        }

        return false;
    }

    public void InterruptCreation()
    {
        //STOP THE PRESS! The player moved too much and the cloning sequence will now stop
        m_fCreationTimer = m_fCreationMaxTime;
        m_bCloneInCreation = false;
        SetSparklesEnabled(false);
    }
}
