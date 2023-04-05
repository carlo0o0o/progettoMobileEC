using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_mushroom : Enemy
{

    

    protected override void Start()
    {
        base.Start();
        
    }
    private void Update()
    {
        WalkAround();
        CollisionChecks();

        anim.SetFloat("xVelocity", rb.velocity.x); 

    }
}
