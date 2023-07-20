using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;

    [SerializeField] private bool pcTesting;

    [Header("Move info")]
    public float moveSpeed;
    public float jumpForce;
    public float doubleJumpForce;
    public Vector2 wallJumpDirection;

    private float defaultJumpForce;

    private bool canDoubleJump = true;   //per il doppio salto

    

    private bool canBeControlled;

    [SerializeField] private float bufferJumpTime;
    private float bufferJumpCounter;

    [SerializeField] private float cayoteJumpTime;
    private float cayoteJumpCounter;
    private bool canHaveCayoteJump;

    private float defaultGravityScale;

    [Header("Knockback info")]
    [SerializeField] private Vector2 knockBackDirection;
    [SerializeField] private float knockBackTime;
    [SerializeField] private float knockbackProtectionTime;
    private bool isKnocked;
    private bool canBeKnocked = true;


    [Header("Collision info")]
    [SerializeField] private LayerMask whatIsGround;
    [SerializeField] private LayerMask whatIsWall;
    [SerializeField] private float groundCheckDistance;
    [SerializeField] private float wallCheckDistance;
    [SerializeField] private Transform enemyCheck;
    [SerializeField] private float enemyCheckRadius;
    private bool isGrounded;
    private bool isWallDetected;
    private bool canWallSlide;
    private bool isWallSliding;


    private bool facingRight = true;
    private bool canMove;

    private int facingDirection = 1;

    [Header("Controlls info")]
    public VariableJoystick joystick;
    private float movingInput;
    private float vInput;



    private void Awake()
    {
        //Debug.Log("Awake was called!");
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        defaultJumpForce = jumpForce;

        defaultGravityScale = rb.gravityScale;
        rb.gravityScale = 0;
    }

    private void FixedUpdate()
    {
       // Debug.Log("FixedUpdate was called!");
    }


    // Update is called once per frame
    void Update()
    {
        AnimationControllers();

        if (isKnocked)
            return;

        FlipController();
        CollisionChecks();
        InputChecks();
        CheckForEnemy();



        bufferJumpCounter -= Time.deltaTime;
        cayoteJumpCounter -= Time.deltaTime;


        if (isGrounded)
        {
            canDoubleJump = true;    //se non rimetto canDoubleJump true posso fare solo un avolta due salti
            canMove = true;

            if (bufferJumpCounter > 0)
            {
                bufferJumpCounter = -1;
                Jump();
            }

            canHaveCayoteJump = true;

        } 
        else
        {
            if (canHaveCayoteJump)
            {
                canHaveCayoteJump = false;
                cayoteJumpCounter = cayoteJumpTime;
            }
            
        }

        if (canWallSlide)
        {
            isWallSliding = true;
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.1f);
        }

        Move();

    }

    private void CheckForEnemy()
    {

        Collider2D[] hitedColliders = Physics2D.OverlapCircleAll(enemyCheck.position, enemyCheckRadius);

        foreach (var enemy in hitedColliders)
        {
            if (enemy.GetComponent<Enemy>() != null)
            {
                Enemy newEnemy = enemy.GetComponent<Enemy>();

                if (newEnemy.invincible)
                    return;

                if(rb.velocity.y < 0)
                {
                    AudioManager.instance.PlaySFX(1);
                    newEnemy.Damage();
                    Jump();
                }
                
            }
        }
    }

    private void AnimationControllers()
    {
        bool isMoving = rb.velocity.x != 0;

        anim.SetBool("isKnocked", isKnocked);
        anim.SetBool("isMoving", isMoving);
        anim.SetBool("isGrounded", isGrounded);
        anim.SetBool("isWallSliding", isWallSliding);
        anim.SetBool("isWallDetected", isWallDetected);
        anim.SetBool("canBeControlled", canBeControlled);
        anim.SetFloat("yVelocity", rb.velocity.y);
    }

    private void InputChecks()
    {
        if (!canBeControlled)
            return;

        if (pcTesting)
        {
            movingInput = Input.GetAxisRaw("Horizontal");
            vInput = Input.GetAxisRaw("Vertical");

        }
        else
        {
            movingInput = joystick.Horizontal;
            vInput = joystick.Vertical;
        }
        if (vInput < 0)
            canWallSlide = false;

        if (Input.GetKeyDown(KeyCode.Space))    // se space =1 chiamo jumpButton
            JumpButton();
    }

    public void ReturnControll()
    {
        rb.gravityScale = defaultGravityScale;
        canBeControlled = true;
    }

    public void JumpButton()
    {

        if (!isGrounded)
            bufferJumpCounter = bufferJumpTime;


        if (isWallSliding)
        {
            WallJump();
            canDoubleJump = true;
        }

        else if (isGrounded || cayoteJumpCounter > 0)
        {
            Jump();
        }
        else if (canDoubleJump)
        {
            canMove = true;
            canDoubleJump = false;
            jumpForce = doubleJumpForce;
            Jump();
            jumpForce = defaultJumpForce;
        }
        canWallSlide = false;
    }


    public void Knockback(Transform damageTransform)
    {
        AudioManager.instance.PlaySFX(10);
        if (!canBeKnocked)
            return;

        //if (GameManager.instance.difficulty > 1)         //solo se la difficolta non è la piu semplice
        //{
            //PlayerManager.instance.fruits--;

            //if(PlayerManager.instance.fruits < 0)
            //{
            //    PlayerManager.instance.KillPlayer();
            //}

            PlayerManager.instance.OnTakingDamege();

        //}


        //GetComponent<CameraShakeFX>().ScreenShake(-facingDirection);    //shake camera

        //isKnocked = true;
        //canBeKnocked = false;

        //#region Define horizontal direction for knockback
        //int hDirection = 0;
        //if (transform.position.x > damageTransform.position.x)
        //    hDirection = 1;
        //else if (transform.position.x < damageTransform.position.x)
        //    hDirection = -1;
        //#endregion

        //rb.velocity = new Vector2(knockBackDirection.x * hDirection, knockBackDirection.y);

        //Invoke("CancelKnockback", knockBackTime);
        //Invoke("AllowKnockback", knockbackProtectionTime);
    }

    private void CancelKnockback()
    {
        isKnocked = false;
    }

    private void AllowKnockback()
    {
        canBeKnocked = true;
    }



    private void Move()
    {
        if(canMove)
             rb.velocity = new Vector2(moveSpeed * movingInput, rb.velocity.y);
    }

    private void WallJump()
    {
        AudioManager.instance.PlaySFX(13);
        canMove = false;
        rb.velocity = new Vector2(wallJumpDirection.x * -facingDirection, wallJumpDirection.y);
    }



    private void Jump()
    {
        AudioManager.instance.PlaySFX(4);
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
    }

    public void Push(float pushForce)
    {
        rb.velocity = new Vector2(rb.velocity.x, pushForce);
    }

    private void FlipController()
    {
        if(facingRight && rb.velocity.x < 0)
            Flip();
        else if(!facingRight && rb.velocity.x > 0)      
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
        isWallDetected = Physics2D.Raycast(transform.position, Vector2.right * facingDirection, wallCheckDistance, whatIsWall);
        
        if(isWallDetected && rb.velocity.y < 0)   //se player sta precipitando
            canWallSlide = true;

        if (!isWallDetected)
        {
            isWallSliding = false;
            canWallSlide = false;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, new Vector3(transform.position.x, transform.position.y - groundCheckDistance));
        Gizmos.DrawLine(transform.position, new Vector3(transform.position.x + wallCheckDistance * facingDirection, transform.position.y));
        Gizmos.DrawWireSphere(enemyCheck.position, enemyCheckRadius);
    }

}
