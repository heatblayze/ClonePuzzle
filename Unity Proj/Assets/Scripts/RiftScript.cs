using UnityEngine;
using System.Collections;

public class RiftScript : MonoBehaviour
{
    //The layer the player and clones are on
    public LayerMask m_lPlayerLayer;

    GameObject m_gObjectToDestroy = null;

    //Used for the cool destroy anim thing
    bool m_bDestroy = false;

    //Used for the cool spawn-tearing effect
    bool m_bStart = true;
    Vector3 m_v3SpawnScale;

    void Start()
    {
        m_v3SpawnScale = new Vector3(0.599161f, transform.localScale.y, transform.localScale.z);
    }

    void Update()
    {
        transform.LookAt(transform.position + Camera.main.transform.rotation * Vector3.back,Camera.main.transform.rotation * Vector3.up);
        if (m_bStart)
        {
            transform.localScale = Vector3.Lerp(transform.localScale, m_v3SpawnScale, 0.05f);
            if (transform.localScale.x > 0.595f)
            {
                m_bStart = false;
            }
        }

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
