using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    private Animator anim;


    [SerializeField] private Transform[] movePoint;
    [SerializeField] private float speed;
    [SerializeField] private float cooldown;

    private int movePointIndex;
    private float coolDownTimer;

    void Start()
    {
        anim = GetComponent<Animator>();
        transform.position = movePoint[0].position;
    }

    // Update is called once per frame
    void Update()
    {

        coolDownTimer -= Time.deltaTime;
        bool isWorking = coolDownTimer < 0;

        anim.SetBool("isWorking", isWorking);

        if (isWorking)
            transform.position = Vector3.MoveTowards(transform.position, movePoint[movePointIndex].position, speed * Time.deltaTime);

        if (Vector2.Distance(transform.position, movePoint[movePointIndex].position) < 0.15f)
        {
            coolDownTimer = cooldown;
            movePointIndex++;
            if (movePointIndex >= movePoint.Length)
            {
                movePointIndex = 0;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Player>() != null)
            collision.transform.SetParent(transform);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<Player>() != null)
            collision.transform.SetParent(null);
    }
}
