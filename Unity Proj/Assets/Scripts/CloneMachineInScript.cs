using UnityEngine;
using System.Collections;

public class CloneMachineInScript : MonoBehaviour
{
    //The layer the player is on
    public int m_iPlayerLayer;

    //Whether the clone has been created
    bool m_bCloneCreated = false;
    //Whether the clone is now being created
    bool m_bCloneInCreation = false;

    //The linked OUT machine
    public GameObject m_gLinkedOutMachine = null;

	// Use this for initialization
	void Start ()
    {
	    
	}
	
	// Update is called once per frame
	void Update ()
    {
	    
	}

    public void StartCreation()
    {
        //IT'S ALIVE!
    }
}
