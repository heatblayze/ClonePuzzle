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

    public List<GameObject> m_gSparkles;

    // Use this for initialization
    void Start()
    {
        for (int i = 0; i < m_gSparkles.Count; i++)
        {
            m_gSparkles[i].gameObject.SetActive(false);
        }
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (CreationAnim())
        {

        }
	}

    public void StartCreation()
    {
        //IT'S ALIVE!
        Instantiate(m_gClonePrefab, m_tCloneInitPos.position, new Quaternion());
        return;
        m_bCloneInCreation = true;
    }

    bool CreationAnim()
    {
        //Once the anim is over, return true
        //otherwise return false to not create the clone yet
        return true;
    }
}
