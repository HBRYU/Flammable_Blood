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

    public Transform[] patrol_MovePos;
    Vector2 patrol_ActiveMovePos;
    public bool patrol_Axis_x;
    public float patrol_Delay;
    private float patrol_Delay_Timer;

    int state_Idle = 0;
    int state_Patrol = 1;
    int state_Chase = 2;

    public int state;

    Vector2 movePos;

    // Start is called before the first frame update
    void Start()
    {
        _GM_ = GameObject.FindGameObjectWithTag("GM").GetComponent<GM>();
        player = _GM_.player;

        patrol_ActiveMovePos = patrol_MovePos[0].position;

        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (state == state_Idle)
            Idle();
        if (state == state_Patrol)
            Patrol();
        if (state == state_Chase)
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
        if(Vector2.Distance(transform.position, patrol_ActiveMovePos) <= 0.1f)
        {
            patrol_Delay_Timer += 1;
            if(patrol_Delay_Timer >= patrol_Delay)
            {

            }
        }

        if(patrol_ActiveMovePos.x > transform.position.x)
        {
            FaceDirection(true);
            TravelInDirection(speed_Patrol, true);
        }
        else
        {
            FaceDirection(false);
            TravelInDirection(speed_Patrol, false);
        }

        if (patrol_Axis_x)
        {
            
        }
    }

    void Chase()
    {

    }
}
