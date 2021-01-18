﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] enemies;
    public GameObject AsteroidPrefab;

    public float xMin, xMax, yPos;
    public float startWait = 3.0f;
    public float spawnWait = 1.0f; 

    float randX;

    void Start()
    {
        InvokeRepeating("SpawnAsteroid", 8, 10);
        StartCoroutine(SpawnEnemies());
    }

    void SpawnAsteroid()
    {
        randX = Random.Range(xMin, xMax);
        Instantiate(AsteroidPrefab, new Vector2(randX, yPos), Quaternion.identity);
    }
    IEnumerator SpawnEnemies()
    {
        yield return new WaitForSeconds(3.0f);

        while (true)
        {
            for(int i = 0; i < enemies.Length; i++)
            {
                randX = Random.Range(xMin, xMax);
                GameObject enemy = enemies[Random.Range(0, enemies.Length)];
                Instantiate(enemy, new Vector2(randX, yPos), Quaternion.identity);

                yield return new WaitForSeconds(spawnWait);
            }
        }
    }
}
