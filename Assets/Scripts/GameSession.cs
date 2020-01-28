using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameSession : MonoBehaviour
{
    [Range(0.1f, 10f)] [SerializeField] float gameSpeed = 1f;
    [SerializeField] int score = 0;
    [SerializeField] int pointsPerBlockDestroyed = 10;
    [SerializeField] Text scoreText;

    private void Awake()
    {
        int gameStatusCount = FindObjectsOfType<GameSession>().Length;
        if (gameStatusCount > 1)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        } else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        scoreText.text = "Score: " + score;
    }

    // Update is called once per frame
    void Update()
    {
        Time.timeScale = gameSpeed;
    }

    public void IncreaseScore()
    {
        score += pointsPerBlockDestroyed;
        scoreText.text = "Score: " + score;
    }

    public void ResetSingleton()
    {
        gameObject.SetActive(false);
        Destroy(gameObject);
    }
}
