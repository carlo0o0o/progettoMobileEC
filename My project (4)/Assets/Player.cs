using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;

    [Header("Move info")]
    public float moveSpeed;
    public float jumpForce;

    private bool canDoubleJump = true;   //per il doppio salto

    private float movingInput;

    [Header("Collision info")]
    public LayerMask whatIsGround;
    public float groundCheckDistance;
    public float wallCheckDistance;
    private bool isGrounded;
    private bool isWallDetected;
    private bool canWallSlide;
    private bool isWallSliding;


    private bool facingRight = true;
    private int facingDirection = 1;


    private void Awake()
    {
        Debug.Log("Awake was called!");
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        Debug.Log("FixedUpdate was called!");
    }


    // Update is called once per frame
    void Update()
    {
        AnimationControllers();
        FlipController();
        CollisionChecks();
        InputChecks();


        if (isGrounded)
            canDoubleJump = true;    //se non rimetto canDoubleJump true posso fare solo un avolta due salti

        if (canWallSlide)
        {
            isWallSliding = true;
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.1f);
        }

        Move();
    }

    private void AnimationControllers()
    {
        bool isMoving = rb.velocity.x != 0;

        anim.SetBool("isMoving", isMoving);
        anim.SetBool("isGrounded", isGrounded);
        anim.SetFloat("yVelocity", rb.velocity.y);
    }

    private void InputChecks()
    {
        movingInput = Input.GetAxis("Horizontal");

        if (Input.GetAxis("Vertical") < 0)
            canWallSlide = false;

        if (Input.GetKeyDown(KeyCode.Space))    // se space =1 chiamo jumpButton
            jumpButton();
    }

    private void jumpButton()
    {
        if (isGrounded)
        {
            Jump();
        }
        else if (canDoubleJump)
        {
            canDoubleJump = false;
            Jump();
        }
    }

    private void Move()
    {
        rb.velocity = new Vector2(moveSpeed * movingInput, rb.velocity.y);
    }

    private void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
    }

    private void FlipController()
    {
        if(facingRight && movingInput < 0)
            Flip();
        else if(!facingRight && movingInput > 0)      
            Flip();
    }

    private void Flip()
    {
        facingDirection = facingDirection * -1;   
        facingRight = !facingRight;               //se e true diventa false e l'opposto
        transform.Rotate(0, 180, 0);
    }

    private void CollisionChecks()
    {
        isGrounded = Physics2D.Raycast(transform.position, Vector2.down, groundCheckDistance, whatIsGround);
        isWallDetected = Physics2D.Raycast(transform.position, Vector2.right * facingDirection, wallCheckDistance, whatIsGround);
        
        if(isWallDetected && rb.velocity.y < 0)   //se player sta precipitando
            canWallSlide = true;

        if (!isWallDetected)
            canWallSlide = false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, new Vector3(transform.position.x, transform.position.y - groundCheckDistance));
        Gizmos.DrawLine(transform.position, new Vector3(transform.position.x + wallCheckDistance * facingDirection, transform.position.y));
    }

}
