using UnityEngine;
using System.Collections;

public class RiftScript : MonoBehaviour
{
    //The layer the player and clones are on
    public LayerMask m_lPlayerLayer;

    void OnTriggerEnter(Collider a_collider)
    {
        //Make sure it's a player or clone
        if (1 << a_collider.gameObject.layer == m_lPlayerLayer)
        {
            //Make sure it's a clone
            if (!a_collider.GetComponent<PlayerContScript>().m_bIsTruePlayer)
            {
                Destroy(a_collider.gameObject);
                Destroy(this.gameObject);
            }
        }
    }
}
