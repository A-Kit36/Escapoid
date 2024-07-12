using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public float awarenessLevel;
    public float lives;
    public bool isGamePaused;

    void Start()
    {
        awarenessLevel = 0f;
        lives = 3f;
    }

    void Update()
    {
        
    }

    public void RaiseAwareness()
    {
        awarenessLevel += 1f;
        if (awarenessLevel >= 10f)
        {
            LoseLive();
            Debug.Log("CAUGHT");
            //add code for restart to checkpoint
        } 
    }

    public void LoseLive()
    {
        lives = lives--;
        if (lives <= 0)
        {
            GameOver();
        }
    }

    public void WinLive()
    {
        lives = lives++;
    }

    public void GameOver()
    {
        Debug.Log("Game Over!");
        //add a reload screen to beginning of game
    }

    public void PauseGame()
    {
        isGamePaused = true;
        Time.timeScale = 0f;
    }

    public void ResumeGame()
    {
        isGamePaused = false;
        Time.timeScale = 1f;
    }

    public void RestartGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
    }
}
