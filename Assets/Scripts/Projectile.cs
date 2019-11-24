using UnityEngine;

public class Projectile : MonoBehaviour
{
    Rigidbody2D body;
    float timer = 2.0f;

    void Awake() {
        body = GetComponent<Rigidbody2D>();
    }

    void OnCollisionEnter2D(Collision2D collision) {
        var enemy = collision.gameObject.GetComponent<Enemy>();
        if (enemy != null)
        {
            enemy.DecreaseHealth();
        }

        Destroy(gameObject);
    }

    void Update() {
        if (timer <= 0)
            Destroy(gameObject);

        timer -= Time.deltaTime;
    }

    public void Launch(Vector2 direction, float force) {
        body.AddForce(direction * force);
    }
}
