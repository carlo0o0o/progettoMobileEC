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
    private bool isGrounded;


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

        CollisionChecks();

        //Debug.Log("Update was called!");

        InputChecks();

        if (isGrounded)
        {
            canDoubleJump = true;    //se non rimetto canDoubleJump true posso fare solo un avolta due salti
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

        if (Input.GetKeyDown(KeyCode.Space))    // se space =1 chiamo jumpButton
        {
            jumpButton();
        }
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

    private void CollisionChecks()
    {
        isGrounded = Physics2D.Raycast(transform.position, Vector2.down, groundCheckDistance, whatIsGround);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, new Vector3(transform.position.x, transform.position.y - groundCheckDistance));
    }

}
