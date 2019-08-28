using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{

    private GameObject player;
    private GM _GM_;
    private Enemy1_Attack e1attack;
    private Enemy1_Animation e1anim;

    private Rigidbody2D rb;

    public Collider2D radar;
    public Collider2D sight;
    public Collider2D cliffCheck;
    public Transform eyes;
    public LayerMask whatIsGround;

    public bool active = true;
    public bool move = true;

    [Header("Stats 스탯")]
    public float attackDistance;

    public float speed_Idle;
    public float speed_Patrol;
    public float speed_Chase;

    [Header("Patrol State 경비 상태")]
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

    bool facingRight;
    bool lastFacingRight;

    public float faceDirectionDelay;
    private float faceDirection_timer;

    void Start()
    {
        _GM_ = GameObject.FindGameObjectWithTag("GM").GetComponent<GM>();
        player = _GM_.player;
        e1attack = GetComponent<Enemy1_Attack>();
        e1anim = GetComponent<Enemy1_Animation>();

        patrol_ActiveMovePos = patrol_MovePos[0];

        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (_GM_.playerAlive == false)
        {
            active = false;
        }

        if (active)
        {
            SetState();
        }
        else
        {
            e1attack.attack = false;
            if (state == "Chase")
                state = "Idle";
            else
                state = "Patrol";
        }
    }

    void SetState()
    {
        RaycastHit2D wallInSight = Physics2D.Raycast(eyes.position, player.transform.position - transform.position, Vector2.Distance(transform.position, player.transform.position), whatIsGround);

        if (radar.IsTouching(GameObject.FindGameObjectWithTag("Player/Hitbox").GetComponent<Collider2D>()) && wallInSight.collider == null)
        {
            state = "Chase";
            Vector2 xSight = player.transform.position;
            //xSight.y = eyes.position.y;
            if (Physics2D.Raycast(eyes.position, transform.right * transform.localScale.x, Mathf.Abs(eyes.position.x - player.transform.position.x), whatIsGround).collider == null)
            {
                e1attack.attack = true;
            }
            else
            {
                e1attack.attack = false;
            }
        }
        else if ((!radar.IsTouching(GameObject.FindGameObjectWithTag("Player/Hitbox").GetComponent<Collider2D>()) && !sight.IsTouching(GameObject.FindGameObjectWithTag("Player/Hitbox").GetComponent<Collider2D>())) || wallInSight.collider != null)
        {
            e1attack.attack = false;
            if (state == "Chase")
                state = "Idle";
            else
                state = "Patrol";
        }
    }

    void FixedUpdate()
    {
        if (move)
        {
            if (state == "Idle")
                Idle();
            if (state == "Patrol")
                Patrol();
            if (state == "Chase")
                Chase();
        }
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
        if(speed != 0)
            e1anim.Move(true);
        else
            e1anim.Move(false);
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
        if(Mathf.Abs(transform.position.x - patrol_ActiveMovePos.position.x) <= 0.3f)
        {
            patrol_Moving = false;
        }
        if(Mathf.Abs(transform.position.x - patrol_ActiveMovePos.position.x) > 0.3f)
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
            TravelInDirection(0, true);
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
        if (player.transform.position.x > transform.position.x)
        {
            facingRight = true;
            if (lastFacingRight != facingRight)
            {
                faceDirection_timer += 1;
                if (faceDirection_timer >= faceDirectionDelay)
                {
                    lastFacingRight = facingRight;
                    faceDirection_timer = 0;
                }
            }
            else
            {
                FaceDirection(true);
                FollowPlayer(true);
            }
        }
        else
        {
            facingRight = false;
            if (lastFacingRight != facingRight)
            {
                faceDirection_timer += 1;
                if (faceDirection_timer >= faceDirectionDelay)
                {
                    lastFacingRight = facingRight;
                    faceDirection_timer = 0;
                }
            }
            else
            {
                FaceDirection(false);
                FollowPlayer(false);
            }
        }

        void FollowPlayer(bool faceRight)
        {
            if (cliffCheck.IsTouchingLayers(whatIsGround) && Vector2.Distance(transform.position, player.transform.position) > attackDistance)
            {
                TravelInDirection(speed_Chase, faceRight);
            }
            else if (cliffCheck.IsTouchingLayers(whatIsGround) && Vector2.Distance(transform.position, player.transform.position) <= attackDistance)
            {
                TravelInDirection(0.0f, faceRight);
            }
            else
            {
                TravelInDirection(0.0f, faceRight);
            }
        }
    }

    
}
