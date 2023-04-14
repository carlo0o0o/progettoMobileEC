using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Rino : Enemy
{
    [Header("Rino specific")]
    [SerializeField] private float agroSpeed;
    [SerializeField] private float shockTime;
                     private float shockTimeCounter;

    //[SerializeField] private LayerMask whatToIgnore;
    
    

    protected override void Start()
    {
        base.Start();
        invincible = true;
    }


    private void Update()
    {

        CollisionChecks();

        
        if (playerDetection.collider.GetComponent<Player>() != null)
            aggresive = true;

        if (!aggresive)
        {
            WalkAround();
        }
        else
        {
            if (!groundDetected)
            {
                aggresive = false;
                Flip();
            }

            rb.velocity = new Vector2(agroSpeed * facingDirection, rb.velocity.y);

            if(wallDetected && invincible)
            {
                invincible = false;
                shockTimeCounter = shockTime;
            }

            if(shockTimeCounter<=0 && !invincible)
            {
                invincible = true;
                Flip();
                aggresive = false;
            }

            shockTimeCounter -= Time.deltaTime;
        }

        AnimatorControllers();

    }

    private void AnimatorControllers()
    {
        anim.SetBool("invincible", invincible);
        anim.SetFloat("xVelocity", rb.velocity.x);
    }

    
}
