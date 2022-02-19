using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameSession : MonoBehaviour
{
    [SerializeField] int playerLives = 3;
    [SerializeField] TextMeshProUGUI livesText;
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] int pickedCoinScore;

    void Awake()
    {
        int numOfGameSessions = FindObjectsOfType<GameSession>().Length;
        if(numOfGameSessions > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Start()
    {
        livesText.text = playerLives.ToString();
        scoreText.text = pickedCoinScore.ToString();
    }

    public void AddToScore(int pointsToAdd)
    {
        pickedCoinScore += pointsToAdd;
        scoreText.text = pickedCoinScore.ToString();
    }

    public void ProcessPlayerDeath()
    {
        if(playerLives > 1)
        {
            TakeLife();
        }
        else
        {
            ResetGameSession();            
        }
    }

    private void ResetGameSession()
    {
        FindObjectOfType<ScenePersist>().RestartPersist();
        SceneManager.LoadScene(0);
        Destroy(gameObject);
    }

    private void TakeLife()
    {
        playerLives--;
        int activeScene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(activeScene);
        livesText.text = playerLives.ToString();
    }
}
