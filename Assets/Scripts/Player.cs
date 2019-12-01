using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public int maxHealth = 10;
    public int maxAmmo = 50;
    public float speed = 3.0f;
    public GameObject projectilePrefab;
    public ParticleSystem gunSmokeEffect;

    Rigidbody2D body;
    Animator animator;
    Text healthText;
    Text ammoText;

    private Vector2 lookDirection = new Vector2(1, 0);

    private int health = 5;
    private int ammo = 15;
    private UnityEvent healthChanged;
    private UnityEvent ammoChanged;

    public int Health {
        get { return health; }
        set {
            health = value;
            healthChanged.Invoke();
        }
    }

    public int Ammo {
        get { return ammo; }
        set {
            ammo = value;
            ammoChanged.Invoke();
        }
    }

    #region Unity API
    void Awake() {
        body = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        healthText = GameObject.Find("HealthText").GetComponent<Text>();
        ammoText = GameObject.Find("AmmoText").GetComponent<Text>();
    }

    void Start() {
        if (healthChanged == null)
            healthChanged = new UnityEvent();
        if (ammoChanged == null)
            ammoChanged = new UnityEvent();

        healthChanged.AddListener(OnHealthChanged);
        healthChanged.AddListener(CheckGameOver);
        OnHealthChanged();

        ammoChanged.AddListener(OnAmmoChanged);
        OnAmmoChanged();
    }

    void OnDisable() {
        healthChanged.RemoveAllListeners();
        ammoChanged.RemoveAllListeners();
    }

    void Update() {
        if (GameManager.instance.gameOver)
            return;

        var horizontal = Input.GetAxis("Horizontal");
        var vertical = Input.GetAxis("Vertical");
        var move = new Vector2(horizontal, vertical);

        body.MovePosition(body.position + move * speed * Time.fixedDeltaTime);

        if (!Mathf.Approximately(move.x, 0f) || !Mathf.Approximately(move.y, 0f)) {
            lookDirection.Set(move.x, move.y);
            lookDirection.Normalize();
        }

        animator.SetFloat("Look X", lookDirection.x);
        animator.SetFloat("Look Y", lookDirection.y);

        if (Input.GetKeyDown(KeyCode.Space))
            Launch();
    }
    #endregion

    void Launch() {
        if (Ammo == 0)
            return;

        var projectileObject = Instantiate(projectilePrefab, body.position + lookDirection * .5f, Quaternion.identity);

        Projectile projectile = projectileObject.GetComponent<Projectile>();
        projectile.Launch(lookDirection, 600f);

        animator.SetTrigger("Shoot");
        gunSmokeEffect.Play();

        Ammo--;
    }

    void OnHealthChanged() {
        healthText.text = $"Health: { Health }";
    }

    void CheckGameOver() {
        if (Health <= 0)
            GameManager.instance.GameOver();
    }

    void OnAmmoChanged()
    {
        ammoText.text = $"Ammo: { Ammo }";
    }
}


