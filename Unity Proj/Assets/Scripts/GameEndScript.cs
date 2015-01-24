using UnityEngine;
using System.Collections;

public class GameEndScript : MonoBehaviour
{
    //Wait this time at the start of a level before checking the clones
    float m_fDefaultWaitTimer = 5f;

	// Use this for initialization
	void Start ()
    {
	    
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (m_fDefaultWaitTimer > 0)
        {
            m_fDefaultWaitTimer -= Time.deltaTime;
        }
        else
        {
            GameObject[] list = GameObject.FindObjectsOfType<GameObject>();
            bool foundAny = false;
            for (int i = 0; i < list.Length; ++i)
            {
                if (list[i].name.Contains("Player(") || list[i].name.Contains("Player ("))
                {
                    foundAny = true;
                    continue;
                }
            }
            if (!foundAny)
            {
                Application.LoadLevel(Application.loadedLevel + 1);
            }
        }
	}
}
