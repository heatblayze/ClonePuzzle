using UnityEngine;
using System.Collections;

public class PlayerContScript : MonoBehaviour
{
    //Am I the one true player?
    public bool m_bIsTruePlayer = false;

    //The slowest speed the player can move
    public float m_fSpeed = 5f;

    //A selector for the speeds, multiplies with the speed
    int m_iSpeedState = 2;

    //Are the controls inverted?
    bool m_bInverted = false;
    
    //The current facing of the player (false = left, true = right)
    bool m_bDirection = true;


    //Can this player be controlled
    public bool m_bCanControl = true;
    
    //Whether the player is currently touching the ground
    bool m_bGrounded = false;
    //The position of the ground check object
    public Transform m_tGroundCheck;
    //The radius the check will use
    public float m_fGCheckRad = 0.1f;
    //The layer(s) the check will test
    public LayerMask m_lGroundLayer;

    //The force applied when the player jumps
    float m_fJumpForce = 190f;


    //For rotating the guy around on the Y axis primarily
    Quaternion m_qRotateTarget;
    float m_fStartingRot = 0f;
    float m_fRotateSpeed = 0.2f;

    //For ending the level
    Vector3 m_v3EndTarget;
    bool m_bEndLevel = false;

	// Use this for initialization
	void Start ()
    {
        m_fStartingRot = transform.rotation.eulerAngles.y;
        m_qRotateTarget = Quaternion.Euler(0, m_fStartingRot, 0);
        if (!m_bIsTruePlayer)
        {
            GetComponent<AudioListener>().enabled = false;
        }
	}

    //Fixed update works better for physics calculations as it occurs in a fixed time-step
    void FixedUpdate()
    {
        m_bGrounded = Physics.OverlapSphere(m_tGroundCheck.position, m_fGCheckRad, m_lGroundLayer).Length > 0 ? true : false;

        GetComponent<Animator>().SetBool("Grounded", !m_bGrounded);

        if (!m_bCanControl)
        {
            return;
        }

        float moveVal = Input.GetAxis("Horizontal");

        if (moveVal > 0 && m_bDirection == false)
        {
            m_qRotateTarget = Quaternion.Euler(0, m_fStartingRot, 0);
        }
        else if (moveVal < 0 && m_bDirection == true)
        {
            m_qRotateTarget = Quaternion.Euler(0, m_fStartingRot + 180, 0);
        }

        if (m_bInverted)
        {
            moveVal *= -1;
        }

        GetComponent<Animator>().SetFloat("Speed", Mathf.Abs(moveVal));

        if (Mathf.Abs(moveVal) < 0.5f)
        {
            GetComponent<Animator>().SetFloat("Speed", -1f);
        }

        //If not on ground and not really moving, then you can't move any more
        if (!m_bGrounded && (rigidbody.velocity.x > -0.1f && rigidbody.velocity.x < 0.1f))
        {
            if(SameDir(moveVal))
                return;
        }

        Vector3 newVel = rigidbody.velocity;
        newVel.x = (moveVal * (m_bGrounded ? m_fSpeed : m_fSpeed / 1.5f)) * m_iSpeedState;
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
        if (m_bGrounded && Input.GetAxis("Jump") > 0 && m_bCanControl)
        {
            rigidbody.AddForce(new Vector3(0, m_fJumpForce, 0));
        }

        //Restart!
        if (Input.GetKeyDown(KeyCode.R) == true)
        {
            Application.LoadLevel(Application.loadedLevel);
        }

        //Quit!
        if (Input.GetKeyDown(KeyCode.Escape) == true)
        {
            Application.Quit();
        }

        if (m_qRotateTarget.eulerAngles.y < transform.rotation.eulerAngles.y - 1 || m_qRotateTarget.eulerAngles.y > transform.rotation.eulerAngles.y + 1)
        {
            Quaternion quart = Quaternion.Euler(0, Mathf.Lerp(transform.rotation.eulerAngles.y, m_qRotateTarget.eulerAngles.y, m_fRotateSpeed), 0);
            transform.rotation = quart;
        }

        if (m_bEndLevel)
        {
            transform.position = Vector3.Lerp(transform.position, m_v3EndTarget, m_fRotateSpeed / 20);
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

    //Increase the speed by increasing the multiplication amount
    public void ReduceSpeed()
    {
        if (m_iSpeedState > 1)
        {
            --m_iSpeedState;
        }
    }
    
    //And do the opposite here
    public void IncreaseSpeed()
    {
        if (m_iSpeedState < 3)
        {
            ++m_iSpeedState;
        }
    }

    //Invert the controls from what they currently are
    public void InvertControls()
    {
        m_bInverted = !m_bInverted;
    }

    public void EndLevel()
    {
        if (m_bIsTruePlayer && !m_bEndLevel)
        {
            m_qRotateTarget = Quaternion.Euler(0, m_fStartingRot - 90, 0);
            GetComponent<Animator>().SetFloat("Speed", 1f);
            m_bEndLevel = true;
            m_v3EndTarget = transform.position;
            m_v3EndTarget.z += 5;
        }
    }

    public bool WalkToPoint(Vector3 a_position)
    {
        float dist = Vector3.Distance(transform.position, a_position);
        if (dist < 1.5f)
        {
            GetComponent<Animator>().SetFloat("Speed", -1);
            return true;
        }
        if (a_position.x < transform.position.x)
        {
            m_qRotateTarget = Quaternion.Euler(0, m_fStartingRot + 180, 0);
        }
        else
        {
            m_qRotateTarget = Quaternion.Euler(0, m_fStartingRot, 0);
        }
        float y = transform.position.y;
        Vector3 vec = Vector3.Lerp(transform.position, a_position, m_fRotateSpeed / 15);
        vec.z = 0;
        vec.y = y;
        transform.position = vec;
        
        GetComponent<Animator>().SetFloat("Speed", 1);
        return false;
    }
}
