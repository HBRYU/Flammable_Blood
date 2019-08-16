using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneMovement : MonoBehaviour
{
    private GM _GM_;
    private Rigidbody2D rb;
    private GameObject player;

    public LayerMask whatIsGround;

    public float hoverHeight;
    public float hoverForce;
    public float speed;
    public float acceleration;
    public float retreat_acceleration;

    public string state;

    [Header("Chase")]
    public float alertDistance;
    public float stopDistance;
    public float retreatDistance;

    void Start()
    {
        _GM_ = GameObject.FindGameObjectWithTag("GM").GetComponent<GM>();
        player = _GM_.player;
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        Hover();

        RaycastHit2D wallInSight = Physics2D.Raycast(transform.position, player.transform.position - transform.position, Vector2.Distance(transform.position, player.transform.position), whatIsGround);
        if(Vector2.Distance(transform.position, player.transform.position) < alertDistance && wallInSight.collider == null)
        {
            state = "Chasing";
            Chase();
        }
    }

    void Hover()
    {
        RaycastHit2D ground = Physics2D.Raycast(transform.position, -Vector2.up, hoverHeight, whatIsGround);
        if (ground.collider != null)
        {
            if (transform.position.y - ground.point.y < hoverHeight)
            {
                rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y + (hoverHeight - (transform.position.y - ground.point.y)) * hoverForce);
            }
        }
    }
    
    void Chase()
    {
        bool facingRight;
        //////////////////////////////////Face player
        if(player.transform.position.x - transform.position.x <= 0)
        {
            Quaternion rotator = transform.localRotation;
            rotator.y = 0;
            transform.localRotation = rotator;
            facingRight = false;
        }
        else
        {
            Quaternion rotator = transform.localRotation;
            rotator.y = 180;
            transform.localRotation = rotator;
            facingRight = true;
        }
        ////////////////////////////////////////////////////

        if(Vector2.Distance(transform.position, player.transform.position) > stopDistance)
        {
            TravelTowardsPlayer();
        }
        else if(Vector2.Distance(transform.position, player.transform.position) > retreatDistance)
        {
            Stop();
        }
        else
        {
            if (facingRight)
            {
                rb.velocity = new Vector2(rb.velocity.x - retreat_acceleration, rb.velocity.y);
            }
            else
            {
                rb.velocity = new Vector2(rb.velocity.x + retreat_acceleration, rb.velocity.y);
            }
        }

        //////////////////////////////////////// Internal Functions
        void TravelTowardsPlayer()
        {
            if (facingRight)
            {
                if (rb.velocity.x < speed && speed - rb.velocity.x > acceleration)
                {
                    rb.velocity = new Vector2(rb.velocity.x + acceleration, rb.velocity.y);
                }
                else if (rb.velocity.x < speed && speed - rb.velocity.x <= acceleration)
                {
                    rb.velocity = new Vector2(rb.velocity.x + (speed - rb.velocity.x), rb.velocity.y);
                }
            }
            else
            {
                if (rb.velocity.x > -speed && -speed - rb.velocity.x < -acceleration)
                {
                    rb.velocity = new Vector2(rb.velocity.x - acceleration, rb.velocity.y);
                }
                else if (rb.velocity.x > -speed && -speed - rb.velocity.x >= -acceleration)
                {
                    rb.velocity = new Vector2(rb.velocity.x + (-speed - rb.velocity.x), rb.velocity.y);
                }
            }
        }

        void Stop()
        {
            if (facingRight)
            {
                if (rb.velocity.x > acceleration)
                    rb.velocity = new Vector2(rb.velocity.x - acceleration, rb.velocity.y);
                //else
                   // rb.velocity = new Vector2(0, rb.velocity.y);
            }
            else
            {
                if (rb.velocity.x < -acceleration)
                    rb.velocity = new Vector2(rb.velocity.x + acceleration, rb.velocity.y);
                //else
                    //rb.velocity = new Vector2(0, rb.velocity.y);
            }
        }
    }
}
