using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static int currentDamage = 1;
    public static int currentHealth;

    public float xMin, xMax, yMin, yMax;
    public float timeInvincible = 2.0f;
    public float shootDelay = 0.5f;
    public float speed = 4.0f;

    public GameObject laser;
    public GameObject explosionPrefab;

    float horizontal, vertical;
    float invincibleCooldown;
    float startWait = 3.0f;

    bool isInvincible = false;

    int maxHealth = 3;
    int maxDamage = 3;
    
    Rigidbody2D rb2d;
    Renderer render;

    void Start()
    { 
        rb2d = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;
        render = gameObject.GetComponent<Renderer>();

        StartCoroutine(ShootLaser());
    }

    void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        if (isInvincible)
        {
            invincibleCooldown -= Time.deltaTime;
            if (invincibleCooldown <= 0)
            {
                isInvincible = false;
            }
        }
    }

    void FixedUpdate()
    {
        Move();
    }

    void Move()
    {
        Vector2 position = rb2d.position;
        position.x += speed * horizontal * Time.deltaTime;
        position.y += speed * vertical * Time.deltaTime;

        position.x = Mathf.Clamp(position.x, xMin, xMax);
        position.y = Mathf.Clamp(position.y, yMin, yMax);

        rb2d.MovePosition(position);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("Asteroid"))
        {
            ChangeHealth(-1);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Bomb") || collision.gameObject.CompareTag("Bullet"))
        {
            ChangeHealth(-1);
        }
        else if (collision.gameObject.CompareTag("HealthItem"))
        {
            if (currentHealth >= maxHealth) return;
            else ChangeHealth(1);           
        }
        else if (collision.gameObject.CompareTag("AttackSpeedItem"))
        {
            if (shootDelay <= 0.25f) return;
            else shootDelay -= 0.05f;           
        }
        else if (collision.gameObject.CompareTag("AttackDamageItem"))
        {
            if (currentDamage >= maxDamage) return;
            else ++currentDamage;
        }
        else return;

        Destroy(collision.gameObject);
    }    

    void ChangeHealth(int amount)
    {
        if (amount < 0)
        {
            if (isInvincible) return;

            StartCoroutine(OnDamage());
            StopCoroutine(ShootLaser());

            isInvincible = true;
            invincibleCooldown = timeInvincible;

            currentDamage = 1;
            shootDelay = 0.5f;

            GameObject explosion = Instantiate(explosionPrefab, rb2d.position, Quaternion.identity) as GameObject;
            Destroy(explosion, 0.8f);
        }

        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);

        GameController.Instance.FighterHealth();

        if (currentHealth == 0)
        {
            GameController.Instance.FighterDead();
            Destroy(gameObject);
        }
    }

    IEnumerator OnDamage()
    {
        int cnt = 0;
        while(cnt < 4)
        {
            render.material.color = new Color(255, 255, 255, 0);
            yield return new WaitForSeconds(0.25f);
            render.material.color = new Color(1, 1, 1, 1);
            yield return new WaitForSeconds(0.25f);

            ++cnt;
        }
    }

    IEnumerator ShootLaser()
    {
        yield return new WaitForSeconds(startWait); //3.0f
        while (true)
        {
            Instantiate(laser, rb2d.position + Vector2.up * 1.2f, Quaternion.identity);

            yield return new WaitForSeconds(shootDelay); // 0.3f
        }
    }
}
