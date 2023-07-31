using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameControl : MonoBehaviour
{
    public static GameControl current;
    public ColumnSpawn columnSpawner;
    public bool isGameOver = false;
    public Text scoreText;
    int score = 0;
    public GameObject gameOverText;
    AudioSource BGM;

    void Awake()
    {
        if (current == null)
            current = this;
        else if (current != this)
            Destroy(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        BGM = GetComponent<AudioSource>();
        scoreText.text = "score: " + score; // 開始時の得点を０にする。
    }

    // Update is called once per frame
    void Update()
    {
        if (isGameOver && Input.anyKey)
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void BirdDied()
    {
        columnSpawner.StopSpawn();
        isGameOver = true;
        gameOverText.SetActive(true);
        BGM.Stop();
    }

    public void Scored()
    {
        if (isGameOver) return;
        score++;
        scoreText.text = "Score: " + score;
    }
}
