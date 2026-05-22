using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    [SerializeField] private List<GameLevel> gameLevelList;
    private static int levelNumber = 1;
    private int score;
    private float time;
    private bool isTimerActive;
    public static GameManager Instance { get; private set; }

    private void Update()
    {
        if (isTimerActive)
        {
            time += Time.deltaTime;
        }
    }
    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        Lander.Instance.OnCoinPickup += Lander_OnCoinPickup;
        Lander.Instance.OnLanded += Lander_OnLanded;
        Lander.Instance.OnStateChanged += Lander_OnStateChanged;
        LoadCurrentLevel();
    }

    private void LoadCurrentLevel()
    {
        foreach (GameLevel gameLevel in gameLevelList)
        {
            if (gameLevel.GetLevelNumber() == levelNumber)
            {
                GameLevel spawnedGameLevel = Instantiate(gameLevel, Vector3.zero, Quaternion.identity);
                Lander.Instance.transform.position = spawnedGameLevel.GetLanderStartPosition();
            }
        }
    }

    private void Lander_OnStateChanged(object sender, Lander.OnStateChangedEventArgs e)
    {
        isTimerActive = e.state == Lander.State.Normal;
    }

    private void Lander_OnLanded(object sender, Lander.OnLandedEventArgs e)
    {
        AddScore(e.score);
    }

    private void Lander_OnCoinPickup(object sender, EventArgs e)
    {
        AddScore(500);
    }

    public void AddScore(int addScoreAmount)
    {
        score += addScoreAmount;
        Debug.Log(score);
    }
    public int GetScore()
    {
        return score;
    }

    public float GetTime()
    {
        return time;
    }
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1f; // Resume the game if it was paused
    }

    public void GoToNextLevel()
    {
        levelNumber++;
        SceneManager.LoadScene(0);
    }
    public void RetryLevel()
    {
        SceneManager.LoadScene(0);
    }

    public int GetLevelNumber()
    {
        return levelNumber;
    }
    // You can add additional game management logic here, such as handling game states, managing player lives, etc.
}
