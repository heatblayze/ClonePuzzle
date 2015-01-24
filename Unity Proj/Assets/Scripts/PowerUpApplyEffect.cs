using UnityEngine;
using System.Collections;

public class PowerUpApplyEffect : MonoBehaviour
{
    //The powerup type as an int
    public int m_iPowerupType = 0;

    //A variable for the player layer
    public LayerMask m_lPlayerLayer;

    void OnTriggerEnter(Collider a_collider)
    {
        //Is the collided object a player?
        if (1 << a_collider.gameObject.layer == m_lPlayerLayer.value)
        {
            //What power is it? Apply the correct effect for its type
            switch (m_iPowerupType)
            {
                case 0:
                    {
                        a_collider.GetComponent<PlayerContScript>().IncreaseSpeed();
                        break;
                    }
                case 1:
                    {
                        a_collider.GetComponent<PlayerContScript>().ReduceSpeed();
                        break;
                    }
                case 2:
                    {
                        a_collider.GetComponent<PlayerContScript>().InvertControls();
                        break;
                    }
                default: 
                    break;
            }
            //Destroy the powerup
            Destroy(this.gameObject);
        }
    }
}
