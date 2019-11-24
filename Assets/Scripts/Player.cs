using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed = 3.0f;
    public GameObject projectilePrefab;

    Rigidbody2D body;
    Animator animator;
    Vector2 lookDirection = new Vector2(1, 0);

    void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        var horizontal = Input.GetAxis("Horizontal");
        var vertical = Input.GetAxis("Vertical");
        var move = new Vector2(horizontal, vertical);

        body.MovePosition(body.position + move * speed * Time.fixedDeltaTime);

        if (!Mathf.Approximately(move.x, 0f) || !Mathf.Approximately(move.y, 0f))
        {
            lookDirection.Set(move.x, move.y);
            lookDirection.Normalize();
        }

        animator.SetFloat("Look X", lookDirection.x);
        animator.SetFloat("Look Y", lookDirection.y);

        if (Input.GetKeyDown(KeyCode.Space))
            Launch();
    }

    void Launch()
    {
        var projectileObject = Instantiate(projectilePrefab, body.position + lookDirection * .5f, Quaternion.identity);

        Projectile projectile = projectileObject.GetComponent<Projectile>();
        projectile.Launch(lookDirection, 600f);

        animator.SetTrigger("Shoot");
    }

}


