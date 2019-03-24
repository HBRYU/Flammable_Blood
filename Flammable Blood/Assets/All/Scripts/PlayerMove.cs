using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    private Rigidbody2D rb;
    public float speed;
    public float jumpForce;
    private bool facingRight = true;
    private float moveInput;

    public Transform groundCheck;
    public float groundCheckRadius;
    public LayerMask whatIsGround;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //////////////////////////////////// 좌우 움직임
        moveInput = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(moveInput * speed * Time.deltaTime, rb.velocity.y);
        
        /////////////////////////////////// 좌우 반전
        if(facingRight && moveInput < 0)
        {
            Flip();
        }
        else if (!facingRight && moveInput > 0)
        {
            Flip();
        }

        //////////////////////////////////// 점프
        Collider2D onGround = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);

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
