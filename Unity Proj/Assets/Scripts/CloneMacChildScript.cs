using UnityEngine;
using System.Collections;

public class CloneMacChildScript : MonoBehaviour
{
    //The layer the player is on
    public LayerMask m_lPlayerLayer;

    void OnTriggerEnter(Collider a_collider)
    {
        if (1 << a_collider.gameObject.layer == m_lPlayerLayer.value)
        {
            //A player or clone has entered the IN machine
            transform.parent.GetComponent<CloneMachineInScript>().SetAllowCreation(true);
        }
    }

    void OnTriggerExit(Collider a_collider)
    {
        if (1 << a_collider.gameObject.layer == m_lPlayerLayer.value)
        {
            //A player or clone has exited the IN machine
            transform.parent.GetComponent<CloneMachineInScript>().SetAllowCreation(false);
            transform.parent.GetComponent<CloneMachineInScript>().InterruptCreation();
        }
    }
}
