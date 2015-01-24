using UnityEngine;
using System.Collections;

public class CloneMacChildScript : MonoBehaviour
{
    //The layer the player is on
    public LayerMask m_lPlayerLayer;

	// Use this for initialization
	void Start ()
    {
	    
	}
	
	// Update is called once per frame
	void Update ()
    {
	    
	}

    void OnTriggerEnter(Collider a_collider)
    {
        if (1 << a_collider.gameObject.layer == m_lPlayerLayer.value)
        {
            transform.parent.GetComponent<CloneMachineInScript>().SetAllowCreation(true);
        }
    }

    void OnTriggerExit(Collider a_collider)
    {
        if (1 << a_collider.gameObject.layer == m_lPlayerLayer.value)
        {
            transform.parent.GetComponent<CloneMachineInScript>().SetAllowCreation(false);
        }
    }
}
