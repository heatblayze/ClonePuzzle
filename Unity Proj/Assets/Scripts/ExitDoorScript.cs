using UnityEngine;
using System.Collections;

public class ExitDoorScript : MonoBehaviour
{
    //The value for the door being opened/closed
    bool m_bOpen = false;

    //Transform used to check for the player being in front of the object
    public Transform m_tPlayerCheck;
    //The radius for this check
    public float m_fCheckRad = 0.1f;
    //The layer for this check
    public LayerMask m_lPlayerLayer;

	// Use this for initialization
	void Start ()
    {
	    
	}
	
	// Update is called once per frame
	void Update ()
    {
        //Check the button for going through door (and obviously only if the door is open)
        if (m_bOpen && Input.GetAxis("Vertical") > 0)
        {
            //Make sure the player is in front of this object
            if (Physics.OverlapSphere(m_tPlayerCheck.position, m_fCheckRad, m_lPlayerLayer).Length > 0)
            {
                //End Level
                Debug.Log("End level");
            }
        }
	}

    //Set whether the door is open
    public void SetActive(bool a_val)
    {
        m_bOpen = a_val;
    }
}
