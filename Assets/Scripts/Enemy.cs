using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Movement
{
    Left,
    Right,
    Up,
    Down
}

public class Enemy : MonoBehaviour
{
    public int health = 3;
    public Transform player;
    public float speed = 2.0f;
    public float seeRange = 3.0f;
    public float moveRange = 5f;

    private Vector3 moveTo;
    private bool moving = false;
    private float[] moveRangeArray = new float[4];

    Animator animator;
    Rigidbody2D body;

    void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Start()
    {
        CalculateMoveRange();
    }

    void CalculateMoveRange()
    {
        moveRangeArray[(int)Movement.Left] = transform.position.x - moveRange;
        moveRangeArray[(int)Movement.Right] = transform.position.x + moveRange;
        moveRangeArray[(int)Movement.Up] = transform.position.y + moveRange;
        moveRangeArray[(int)Movement.Down] = transform.position.y - moveRange;
    }

    void Update()
    {
        if (IsPlayerSeen())
        {
            FollowPlayer();
            CalculateMoveRange();

            var diff = player.position - transform.position;
            diff.Normalize();
            animator.SetFloat("Look X", diff.x);
            animator.SetFloat("Look Y", diff.y);
        }
        else
        {
            if (transform.position == moveTo)
            {
                moving = false;
            }

            if (!moving)
                CalculateRandomMovement();
            else
                body.MovePosition(Vector3.MoveTowards(transform.position, moveTo, speed * Time.deltaTime));

            var diff = moveTo - transform.position;
            diff.Normalize();
            animator.SetFloat("Look X", diff.x);
            animator.SetFloat("Look Y", diff.y);
        }

    }

    bool IsPlayerSeen()
    {
        return Vector3.Distance(transform.position, player.position) <= seeRange;
    }

    void CalculateRandomMovement()
    {
        moveTo = Vector3.zero;

        if (transform.position.x >= moveRangeArray[(int)Movement.Right])
            moveTo = transform.position + Vector3.left;
        if (transform.position.x <= moveRangeArray[(int)Movement.Left])
            moveTo = transform.position + Vector3.right;
        if (transform.position.y >= moveRangeArray[(int)Movement.Up])
            moveTo = transform.position + Vector3.down;
        if (transform.position.y <= moveRangeArray[(int)Movement.Down])
            moveTo = transform.position + Vector3.up;

        if (moveTo == Vector3.zero)
            moveTo = (transform.position + RandomDirection());

        moving = true;
    }

    private Vector3 RandomDirection()
    {
        Movement movement = (Movement)Random.Range(0, 4);

        if (movement == Movement.Left)
            return Vector3.left;
        if (movement == Movement.Right)
            return Vector3.right;
        if (movement == Movement.Up)
            return Vector3.up;
        if (movement == Movement.Down)
            return Vector3.down;

        return Vector3.zero;
    }

    private void FollowPlayer()
    {
        body.MovePosition(Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime));
    }

    public void DecreaseHealth()
    {
        health--;
        if (health <= 0)
            Destroy(gameObject);
    }
}
