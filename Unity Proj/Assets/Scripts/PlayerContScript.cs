using UnityEngine;
using System.Collections;

public class PlayerContScript : MonoBehaviour
{
    //Am I the one true player?
    public bool m_bIsTruePlayer = false;

    //The max speed the player can move
    public float m_fMaxSpeed = 10f;
    
    //The current facing of the player (false = left, true = right)
    bool m_bDirection = true;
    
    //Whether the player is currently touching the ground
    bool m_bGrounded = false;
    //The position of the ground check object
    public Transform m_tGroundCheck;
    //The radius the check will use
    public float m_fGCheckRad = 0.1f;
    //The layer(s) the check will test
    public LayerMask m_lGroundLayer;

    //The force applied when the player jumps
    public float m_fJumpForce = 130f;

	// Use this for initialization
	void Start ()
    {
	
	}

    //Fixed update works better for physics calculations as it occurs in a fixed time-step
    void FixedUpdate()
    {
        m_bGrounded = Physics.OverlapSphere(m_tGroundCheck.position, m_fGCheckRad, m_lGroundLayer).Length > 0 ? true : false;

        float moveVal = Input.GetAxis("Horizontal");

        //If not on ground and not really moving, then you can't move any more
        if (!m_bGrounded && (rigidbody.velocity.x > -0.1f && rigidbody.velocity.x < 0.1f))
        {
            if(SameDir(moveVal))
                return;
        }

        Vector3 newVel = rigidbody.velocity;
        newVel.x = moveVal * (m_bGrounded? m_fMaxSpeed : m_fMaxSpeed / 1.5f);
        rigidbody.velocity = newVel;
        //Set the direction, used for things like rotation
        if (moveVal < 0)
        {
            m_bDirection = false;
        }
        else if (moveVal > 0)
        {
            m_bDirection = true;
        }
    }

	// Update is called once per frame
	void Update ()
    {
        //Camera.main.transform.position = new Vector3(transform.position.x, transform.position.y, Camera.main.transform.position.z);
        if (m_bGrounded && Input.GetAxis("Jump") > 0)
        {
            rigidbody.AddForce(new Vector3(0, m_fJumpForce, 0));
        }
	}

    //Check if moving key is the same as current facing
    bool SameDir(float a_moveVal)
    {
        if (a_moveVal < 0 && m_bDirection == false)
        {
            return true;
        }
        else if (a_moveVal > 0 && m_bDirection == true)
        {
            return true;
        }
        return false;
    }
}
