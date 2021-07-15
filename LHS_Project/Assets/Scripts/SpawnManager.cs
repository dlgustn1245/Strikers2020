using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public static bool bossSpawned = false;

    public GameObject[] enemies;
    public GameObject AsteroidPrefab;
    public GameObject BossPrefab;

    public float xMin, xMax, yPos;
    public float startWait = 3.0f;
    public float spawnWait = 1.0f; 

    float randX;

    void Start()
    {
        InvokeRepeating("SpawnAsteroid", 8, 10);
        StartCoroutine(SpawnEnemies());
    }

    void Update()
    {
        if (GameController.Instance.gameClear || bossSpawned)
        {
            CancelInvoke("SpawnAsteroid");
            StopCoroutine(SpawnEnemies());
        }
        if(GameController.score >= 100 && !bossSpawned)
        {
            bossSpawned = true;
            SpawnBoss();
        }
    }

    void SpawnAsteroid()
    {
        randX = Random.Range(xMin, xMax);
        Instantiate(AsteroidPrefab, new Vector2(randX, yPos), Quaternion.identity);
    }

    void SpawnBoss()
    {
        bossSpawned = true;
        Instantiate(BossPrefab, new Vector2(0, yPos), Quaternion.identity);
    }

    IEnumerator SpawnEnemies()
    {
        yield return new WaitForSeconds(startWait);

        while (true) 
        {
            randX = Random.Range(xMin, xMax + 0.1f); 
            GameObject enemy = enemies[Random.Range(0, enemies.Length)];
            Instantiate(enemy, new Vector2(randX, yPos), Quaternion.identity);

            yield return new WaitForSeconds(spawnWait); 
        }
    }
}
