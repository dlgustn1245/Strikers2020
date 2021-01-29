using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidController : MonoBehaviour
{
    public float speed;
    Rigidbody2D rb2d;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (GameController.Instance.gameOver || SpawnManager.bossSpawned)
        {
            Destroy(gameObject);
        }
    }

    void FixedUpdate()
    {
        Vector2 position = rb2d.position;
        position.y -= speed * Time.deltaTime;

        rb2d.MovePosition(position);
    }

    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
