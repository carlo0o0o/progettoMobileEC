using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_mushroom : Enemy
{

    [Header("Move info")]
    [SerializeField] private float speed;
    [SerializeField] private float idleTime = 2;
    private float idleTimeCounter;

    protected override void Start()
    {
        base.Start();
        facingDirection = -1;
    }
    private void Update()
    {
        if (idleTimeCounter <= 0)
            rb.velocity = new Vector2(speed * facingDirection, rb.velocity.y);
        else
            rb.velocity = new Vector2(0, 0);

        idleTimeCounter -= Time.deltaTime;

        CollisionChecks();

        if (wallDetected || !groundDetected)
        {
            idleTimeCounter = idleTime;
            Flip();
        }

        anim.SetFloat("xVelocity", rb.velocity.x); 

    }
}