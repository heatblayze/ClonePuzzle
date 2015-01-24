using UnityEngine;
using System.Collections;

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

	// Use this for initialization
	void Start ()
    {
	
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
        m_bCloneInCreation = true;
    }

    bool CreationAnim()
    {
        //Once the anim is over, return true
        //otherwise return false to not create the clone yet
        return true;
    }
}
