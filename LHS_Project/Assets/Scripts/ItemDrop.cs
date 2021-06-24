using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDrop : MonoBehaviour
{
    public float xMin, xMax, yMin, yMax;
    public float speed;
    
    Rigidbody2D rb2d;

    float rotate = 0.0f;

    int flagX = 1, flagY = 1;

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
        ItemMove();
    }

    void ItemMove()
    {
        Vector2 position = rb2d.position;

        position.x += speed * flagX * Time.deltaTime;
        position.y -= speed * flagY * Time.deltaTime;

        if (position.x >= xMax || position.x <= xMin)
        {
            flagX = -flagX;
        }
        if (position.y >= yMax || position.y <= yMin)
        {
            flagY = -flagY;
        }

        rb2d.MovePosition(position);

        transform.rotation = Quaternion.Euler(new Vector3(0.0f, 0.0f, rotate));
        rotate += 3.0f;

        Destroy(gameObject, 6.0f);
    }
}