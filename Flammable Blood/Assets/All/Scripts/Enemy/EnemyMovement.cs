using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{

    private GameObject player;
    private GM _GM_;

    private Rigidbody2D rb;

    public float speed_Idle;
    public float speed_Patrol;
    public float speed_Chase;

    public List<Transform> patrol_MovePos;
    private Transform patrol_ActiveMovePos;
    public float patrol_Delay;
    private float patrol_Delay_Timer;
    public float patrol_Timeout;
    private float patrol_Timeout_Timer;
    private bool patrol_Moving = true;

    [Header("Idle, Patrol, Chase")]
    public string state;

    Vector2 movePos;

    // Start is called before the first frame update
    void Start()
    {
        _GM_ = GameObject.FindGameObjectWithTag("GM").GetComponent<GM>();
        player = _GM_.player;

        patrol_ActiveMovePos = patrol_MovePos[0];

        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (state == "Idle")
            Idle();
        if (state == "Patrol")
            Patrol();
        if (state == "Chase")
            Chase();

    }

    void FaceDirection(bool faceRight)
    {
        if (faceRight)
        {
            Vector3 scaler = transform.localScale;
            scaler.x = 1;
            transform.localScale = scaler;
        }
        else
        {
            Vector3 scaler = transform.localScale;
            scaler.x = -1;
            transform.localScale = scaler;
        }
    }

    void TravelInDirection(float speed, bool faceRight) 
    {
        if (faceRight)
        {
            rb.velocity = new Vector2(speed, rb.velocity.y);
        }
        else
        {
            rb.velocity = new Vector2(speed * -1, rb.velocity.y);
        }
    }


    void Idle()
    {

    }

    void Patrol()
    {
        if(Vector2.Distance(transform.position, patrol_ActiveMovePos.position) <= 0.3f)
        {
            patrol_Moving = false;
        }
        else
        {
            patrol_Moving = true;
        }
        if(patrol_Moving)
        {
            if (patrol_ActiveMovePos.position.x > transform.position.x)
            {
                FaceDirection(true);
                TravelInDirection(speed_Patrol, true);
            }
            else
            {
                FaceDirection(false);
                TravelInDirection(speed_Patrol, false);
            }
        }
        else
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
            if (patrol_Delay_Timer >= patrol_Delay)
            {
                SetNextMovePos();
                ResetVariables();
            }
            else
            {
                patrol_Delay_Timer += 1;
            }
        }

        patrol_Timeout_Timer += 1;

        if(patrol_Timeout_Timer >= patrol_Timeout)
        {
            SetNextMovePos();
            ResetVariables();
        }
        
        void SetNextMovePos()
        {
            if (patrol_MovePos.IndexOf(patrol_ActiveMovePos) == patrol_MovePos.Count - 1)
                patrol_ActiveMovePos = patrol_MovePos[0];
            else
                patrol_ActiveMovePos = patrol_MovePos[patrol_MovePos.IndexOf(patrol_ActiveMovePos) + 1];
        }

        void ResetVariables()
        {
            patrol_Moving = true;
            patrol_Delay_Timer = 0.0f;
            patrol_Timeout_Timer = 0.0f;
        }
    }


    void Chase()
    {
        
    }
}
