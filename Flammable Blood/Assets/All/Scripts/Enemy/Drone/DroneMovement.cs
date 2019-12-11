using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneMovement : MonoBehaviour
{
    private GM _GM_;
    private Rigidbody2D rb;
    private GameObject player;
    private DroneAttack attackScript;
    private Drone2Attack attackScript2;
    private DroneAnimation anim;

    public int droneType;

    public LayerMask whatIsGround;

    public bool active = true;
    public bool chase = true;

    public AudioClip alertSFX;

    public bool hover = true;
    public float hoverHeight;
    public float hoverForce;
    public float speed;
    public float acceleration;
    public float retreat_acceleration;

    public string state;

    [Header("Chase")]
    public float alertDistance;
    public float shot_alertDistance;
    public float stopDistance;
    public float retreatDistance;

    void Start()
    {
        _GM_ = GameObject.FindGameObjectWithTag("GM").GetComponent<GM>();
        player = _GM_.player;
        rb = GetComponent<Rigidbody2D>();
        if (droneType == 1)
            attackScript = GetComponent<DroneAttack>();
        else
            attackScript2 = GetComponent<Drone2Attack>();

        anim = GetComponent<DroneAnimation>();
    }

    void FixedUpdate()
    {
        if(hover)
            Hover();

        if(_GM_.playerAlive == false)
        {
            active = false;
        }

        if (active && chase)
        {
            RaycastHit2D wallInSight = Physics2D.Raycast(transform.position, player.transform.position - transform.position, Vector2.Distance(transform.position, player.transform.position), whatIsGround);
            if (GM.CompareDistance(transform.position, player.transform.position, alertDistance) <= 0 && wallInSight.collider == null)
            {
                if (state != "Chasing")
                    GetComponent<AudioSource>().PlayOneShot(alertSFX);

                state = "Chasing";
                Chase();
                if(droneType == 1)
                    attackScript.attack = true;
                else
                {
                    attackScript2.attack = true;
                    attackScript2.ResetTempDir();
                }
                anim.Alerted(true);
            }
            else
            {
                if (droneType == 1)
                    attackScript.attack = false;
                else
                    attackScript2.attack = false;
                anim.Alerted(false);
            }
        }
        else if(!active)
        {
            if (droneType == 1)
                attackScript.attack = false;
            else
                attackScript2.attack = false;
            anim.Alerted(false);
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
            rotator.y = 180;
            transform.localRotation = rotator;
            facingRight = false;
        }
        else
        {
            Quaternion rotator = transform.localRotation;
            rotator.y = 0;
            transform.localRotation = rotator;
            facingRight = true;
        }
        ////////////////////////////////////////////////////

        if(GM.CompareDistance(transform.position, player.transform.position, stopDistance) == 1)
        {
            TravelTowardsPlayer();
        }
        else if(GM.CompareDistance(transform.position, player.transform.position, retreatDistance) == 1)
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
