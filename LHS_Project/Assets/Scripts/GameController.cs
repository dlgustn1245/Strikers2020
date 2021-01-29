using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController Instance
    {
        get
        {
            if (!instance) return null;
            return instance;
        }
    }
    private static GameController instance = null;
    
    public static int score = 0;

    public GameObject gameoverText;
    public GameObject readyText;
    public GameObject clearText;
    public GameObject bossText;

    public Text scoreText;
    public Text healthText;
    
    public bool gameOver = false;
    public bool gameClear = false;

    int cnt = 0;

    void Awake()
    {
        if (!instance)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); 
        }
        else Destroy(gameObject);
    }

    void Start()
    {
        StartCoroutine(ShowReadyText());
        StartCoroutine(ShowBossText());
    }

    void Update()
    {
        if (gameOver && Input.GetKeyDown("space"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        
        if (BossController.bossDead)
        {
            StageClear();
        }
    }

    public void FighterScored(int num)
    {
        score += num;
        scoreText.text = "Score : " + score;
    }

    public void FighterDead()
    {
        gameoverText.SetActive(true);
        gameOver = true;
    }

    public void FighterHealth()
    {
        healthText.text = "Health : ";
        for (int i = 0; i < PlayerController.currentHealth; i++)
        {
            healthText.text += "♥ ";
        }
    }

    public void StageClear()
    {
        clearText.SetActive(true);
        gameClear = true;
    }

    IEnumerator ShowReadyText()
    {
        while(cnt < 3)
        {
            readyText.SetActive(true);
            yield return new WaitForSeconds(0.5f);
            readyText.SetActive(false);
            yield return new WaitForSeconds(0.5f);

            ++cnt;
        }
        cnt = 0;
    }

    IEnumerator ShowBossText()
    {
        yield return new WaitUntil(() => score >= 100);
        while (cnt < 3)
        {
            bossText.SetActive(true);
            yield return new WaitForSeconds(0.5f);
            bossText.SetActive(false);
            yield return new WaitForSeconds(0.5f);

            ++cnt;
        }
    }
}
