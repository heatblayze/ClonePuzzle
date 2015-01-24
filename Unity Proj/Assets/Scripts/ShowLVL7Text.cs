using UnityEngine;
using System.Collections;

public class ShowLVL7Text : MonoBehaviour
{
    public GameObject m_gRift;
    public GameObject m_gText;

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
        m_gRift.GetComponent<Animator>().enabled = true;
        m_gRift.GetComponent<SpriteRenderer>().enabled = true;
        m_gText.GetComponent<SpriteRenderer>().enabled = true;
        Destroy(this.gameObject);
    }
}
