using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
    public GameObject explosionPrefab;
    public GameObject bulletPrefab;

    public float speed = 1.0f;
    public float moveSpeed = 2.0f;
    public float bulletSpeed;
    public float yPos;

    public int maxStamina = 20;
    public int oneShoting;
    public int score = 500;
    
    public float xMin, xMax;
    public float startWait = 3.0f;
    public float shootWait = 1.0f;

    int currentStamina;
    int moveFlag = 1;

    bool startAttack = false;
    bool callFlag = true;

    Rigidbody2D rb2d;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        currentStamina = maxStamina;
    }

    void Update()
    {
        if (startAttack && callFlag)
        {
            StartCoroutine(Shot()); //call coroutine just once
            callFlag = false;
        }
    }

    void FixedUpdate()
    {
        BossMove();   
    }

    void BossMove()
    {
        Vector2 position = rb2d.position;

        if (!startAttack)
        {
            position.y -= speed * Time.deltaTime;
        }

        if (position.y <= yPos)
        {
            position.y = yPos;
            position.x += moveSpeed * moveFlag * Time.deltaTime;
            if (position.x >= xMax || position.x <= xMin) moveFlag = -moveFlag;
            startAttack = true;
        }
        rb2d.MovePosition(position);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Laser"))
        {
            currentStamina -= PlayerController.currentDamage;

            if (currentStamina <= 0)
            {
                GameController.Instance.FighterScored(score);

                GameObject explosion = Instantiate(explosionPrefab, rb2d.position, Quaternion.identity) as GameObject;
                Destroy(explosion, 0.8f);

                Destroy(gameObject);
                GameController.Instance.StageClear();
            }
            Destroy(collision.gameObject);
        }
    }

    IEnumerator Shot()
    {
        float angle = 360 / oneShoting;
        while (true)
        {
            for (int i = 0; i < oneShoting; i++)
            {
                GameObject bullet = (GameObject)Instantiate(bulletPrefab, this.transform.position, Quaternion.identity);
                bullet.GetComponent<Rigidbody2D>().AddForce(new Vector2(bulletSpeed * Mathf.Cos(Mathf.PI * 2 * i / oneShoting), bulletSpeed * Mathf.Sin(Mathf.PI * i * 2 / oneShoting)));
                bullet.transform.Rotate(new Vector3(0.0f, 0.0f, 360 * i / oneShoting - 90));
                Destroy(bullet, 2.0f);
            }

            yield return new WaitForSeconds(1.0f);
        }
    }
}
