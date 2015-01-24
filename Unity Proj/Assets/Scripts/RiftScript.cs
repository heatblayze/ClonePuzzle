using UnityEngine;
using System.Collections;

public class RiftScript : MonoBehaviour
{
    //The layer the player and clones are on
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
        if (1 << a_collider.gameObject.layer == m_lPlayerLayer)
        {
            if (!a_collider.GetComponent<PlayerContScript>().m_bIsTruePlayer)
            {

            }
        }
    }
}
