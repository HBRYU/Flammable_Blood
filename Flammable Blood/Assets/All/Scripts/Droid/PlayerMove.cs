using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    private Rigidbody2D rb;
    private PlayerAnimControl anim;

    public bool rawMovement;
    public bool faceMouseMovement;

    public float runSpeed;
    public float walkSpeed;
    private float speed;
    public float jumpForce;

    [HideInInspector]
    public bool jumped, hadJumped, crouched, usingJetpack;

    private bool facingRight = true;
    private float moveInput;
    public bool jetpack;
    public float jetpack_force;
    public float jetpack_terminalV;
    public ParticleSystem jetpack_particles;

    public Transform groundCheck;
    public float groundCheckRadius;
    public LayerMask whatIsGround;

    [HideInInspector]
    public Collider2D onGround;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<PlayerAnimControl>();
        //jetpack_particles.Stop();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //////////////////////////////////// 좌우 움직임
        if(rawMovement == true)
            moveInput = Input.GetAxisRaw("Horizontal");    //D 누르면 1, A 누르면 -1
        else
            moveInput = Input.GetAxis("Horizontal");    //D 누르면 1로 서서히 변환, A 누르면 -1로 서서히 변환
        

        if(Input.GetKey("left shift"))
            speed = runSpeed;   //Shift 누르면 스피드는 달리기 속도
        else
            speed = walkSpeed;  //아니면 스피드는 일반 속도


        if(Input.GetKey("a") && Input.GetKey("d"))      //A랑 D 같이 누르면 멈추기
            speed = 0;

        if(Input.GetKey("s") && onGround == true)   //S 누르고 땅에 있으면 움츠리고 멈추기
        {
            crouched = true;
            speed = 0;
        }
        else
            crouched = false;


        rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);

        /////////////////////////////////// 좌우 반전
        if (faceMouseMovement)
        {
            Vector3 mousePos = Input.mousePosition;
            mousePos = Camera.main.ScreenToWorldPoint(mousePos);
            
            if(mousePos.x >= Camera.main.transform.position.x)
                FaceInDirection(true);
            else
                FaceInDirection(false);
        }
        else
        {
            if (facingRight && moveInput < 0)
                Flip();
            else if (!facingRight && moveInput > 0)
                Flip();
        }
        
        /////////////////////////////////   제트팩
        if(onGround == false && Input.GetKeyDown("w"))
        {
            GetComponent<PlayerAudioManager>().Jetpack_SFX(true);
            usingJetpack = true;
        }
        if (usingJetpack && Input.GetKey("w"))
        {
            //Debug.Log("jetpack on, rb.velocity.y: " + rb.velocity.y);
            if(rb.velocity.y <= jetpack_terminalV)
                rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y + jetpack_force);
            jetpack_particles.Play();
        }
        if (usingJetpack == true && !Input.GetKey("w"))
        {
            usingJetpack = false;
            GetComponent<PlayerAudioManager>().Jetpack_SFX(false);
            jetpack_particles.Stop();
        }
    }

    private void Update()
    {
        //////////////////////////////////// 점프
        onGround = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);

        if (Input.GetKeyDown("w") && onGround == true)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            jumped = true;
            anim.Jump();
        }

        if(rb.velocity.y == 0 && onGround == true)
            jumped = false;

    }
    /////////////////////////////////// 좌우 반전 함수
    void Flip()
    {
        facingRight = !facingRight;
        Vector2 scaler = transform.localScale;
        scaler.x *= -1;
        transform.localScale = scaler;
    }

    void FaceInDirection(bool faceRight)
    {
        if (faceRight)
            transform.localScale = new Vector3(1, 1, 1);
        else
            transform.localScale = new Vector3(-1, 1, 1);
    }
}
