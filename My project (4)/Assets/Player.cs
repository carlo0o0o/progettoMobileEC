using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public float moveSpeed;
    public float jumpForce;
    
    public Rigidbody2D rb;

    private float movingInput;

    private void Awake()
    {
        Debug.Log("Awake was called!");
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void FixedUpdate()
    {
        Debug.Log("FixedUpdate was called!");
    }



    // Update is called once per frame
    void Update()
    {
        Debug.Log("Update was called!");

        movingInput = Input.GetAxis("Horizontal");

        if (Input.GetKeyDown(KeyCode.Space))
        {
           rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
        rb.velocity = new Vector2(moveSpeed * movingInput, rb.velocity.y);
        
    }
}
