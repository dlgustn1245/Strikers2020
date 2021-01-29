using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserController : MonoBehaviour
{
    public GameObject flamePrefab;

    public float speed;

    Rigidbody2D rb2d;
    
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (GameController.Instance.gameOver || GameController.Instance.gameClear)
        {
            Destroy(gameObject);
        }
    }

    void FixedUpdate()
    {
        Vector2 position = rb2d.position;
        position.y += speed * Time.deltaTime;

        rb2d.MovePosition(position);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("Boss"))
        {
            GameObject flame = Instantiate(flamePrefab, rb2d.position, Quaternion.identity) as GameObject;
            Destroy(flame, 0.8f);

            Destroy(gameObject);
        }
        else if (collision.gameObject.CompareTag("Bomb") || collision.gameObject.CompareTag("Asteroid"))
        {
            Destroy(gameObject);
        }
    }

    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
