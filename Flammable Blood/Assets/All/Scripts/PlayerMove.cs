using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    private Rigidbody2D rb;
    public float runSpeed;
    public float walkSpeed;
    private float speed;
    public float jumpForce;
    private bool facingRight = true;
    private float moveInput;

    public Transform groundCheck;
    public float groundCheckRadius;
    public LayerMask whatIsGround;

    [HideInInspector]
    public Collider2D onGround;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //////////////////////////////////// 좌우 움직임
        moveInput = Input.GetAxis("Horizontal");    //D 누르면 1, A 누르면 -1

        if(Input.GetKey("left shift")) { speed = runSpeed; }    //Shift 누르면 스피드는 달리기 속도
        else { speed = walkSpeed; } //아니면 스피드는 일반 속도

        if(Input.GetKey("a") && Input.GetKey("d"))      //A랑 D 같이 누르면 멈추기
        {
            speed = 0;
        }


        rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);
        
        /////////////////////////////////// 좌우 반전
        if(facingRight && moveInput < 0)
        {
            Flip();     //
        }
        else if (!facingRight && moveInput > 0)
        {
            Flip();
        }
    }

    private void Update()
    {
        //////////////////////////////////// 점프
        onGround = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);

        if (Input.GetKeyDown("w") && onGround == true)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
    }
    /////////////////////////////////// 좌우 반전 함수
    void Flip()
    {
        facingRight = !facingRight;
        Vector2 scaler = transform.localScale;
        scaler.x *= -1;
        transform.localScale = scaler;
    }
}
