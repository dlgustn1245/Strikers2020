using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
    public static bool bossDead = false;

    public GameObject explosionPrefab;
    public GameObject bulletPrefab;

    public float speed = 1.0f;
    public float bulletSpeed = 4.0f;
    public float yPos;

    public int maxStamina = 20;
    public int oneShoting = 30;
    public int score = 500;

    public float startWait = 3.0f;
    public float shootWait = 1.0f;

    int currentStamina;

    Rigidbody2D rb2d;
    
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        currentStamina = maxStamina;
    }

    void Update()
    {

    }


    void FixedUpdate()
    {
        Vector2 position = rb2d.position;

        position.y -= speed * Time.deltaTime;
        if (position.y <= yPos) position.y = yPos;

        rb2d.MovePosition(position);
    }

    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Laser"))
        {
            currentStamina -= PlayerController.currentDamage;

            if (currentStamina <= 0)
            {
                GameController.Instance.FighterScored(score);

                bossDead = true;

                GameObject explosion = Instantiate(explosionPrefab, rb2d.position, Quaternion.identity) as GameObject;
                Destroy(explosion, 0.8f);

                Destroy(gameObject);
            }
            Destroy(collision.gameObject);
        }
    }
}
