using UnityEngine;
using System.Collections;

public class RiftScript : MonoBehaviour
{
    //The layer the player and clones are on
    public LayerMask m_lPlayerLayer;

    GameObject m_gObjectToDestroy = null;

    //Used for the cool destroy anim thing
    bool m_bDestroy = false;

    void Update()
    {
        GetComponent<Animator>().SetBool("Started", true);
        if (m_gObjectToDestroy != null)
        {
            DestroyImmediate(m_gObjectToDestroy);
            GetComponent<Collider>().enabled = false;
        }
        if (m_bDestroy)
        {
            if (transform.localScale.y > 0)
            {
                transform.localScale = Vector3.Lerp(transform.localScale, new Vector3(0, -1, 0), 0.5f);
            }
            else if(transform.localScale.x < 5)
            {
                transform.localScale = Vector3.Lerp(transform.localScale, new Vector3(6, 0, 0), 0.5f);
            }
            else
            {
                DestroyImmediate(this.gameObject);
            }
        }
    }

    void OnTriggerEnter(Collider a_collider)
    {
        //Make sure it's a player or clone
        if (1 << a_collider.gameObject.layer == m_lPlayerLayer)
        {
            //Make sure it's a clone
            if (!a_collider.GetComponent<PlayerContScript>().m_bIsTruePlayer)
            {
                m_gObjectToDestroy = a_collider.gameObject;
                m_bDestroy = true;
                GetComponent<Collider>().enabled = false;
            }
        }
    }

    void OnTriggerStay(Collider a_collider)
    {
        //Make sure it's a player or clone
        if (1 << a_collider.gameObject.layer == m_lPlayerLayer)
        {
            //Make sure it's a clone
            if (!a_collider.GetComponent<PlayerContScript>().m_bIsTruePlayer)
            {
                m_gObjectToDestroy = a_collider.gameObject;
                m_bDestroy = true;
                GetComponent<Collider>().enabled = false;
            }
        }
    }

    public void StartDestroy()
    {
        m_bDestroy = true;
    }
}
