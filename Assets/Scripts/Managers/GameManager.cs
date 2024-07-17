using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public float AwarenessLevel; //Capitalize public members i.e. AwarenessLevel this belongs in a CharacterAbility Script
    
    [SerializeField]
    private int _maxLives;
    [SerializeField]
    private int _livesLeft;

    private bool _isGamePaused;


    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    void Start()
    {
        AwarenessLevel = 0f;
        _livesLeft = 3;
    }

    public void RaiseAwareness()
    {
        AwarenessLevel += 1f;
        if (AwarenessLevel >= 10f)
        {
            LoseLive();
            Debug.Log("CAUGHT");
            //add code for restart to checkpoint
        } 
    }

    public void LoseLive()
    {
        _livesLeft = _livesLeft--;
        if (_livesLeft <= 0)
        {
            GameOver();
        }
    }

    public void WinLive()
    {
        _livesLeft = _livesLeft++;
    }

    public void GameOver()
    {
        Debug.Log("Game Over!");
        //add a reload screen to beginning of game
    }

    public void PauseGame()
    {
        _isGamePaused = true;
        Time.timeScale = 0f;
        GUIManager.Instance.OnPause();
    }

    public void ResumeGame()
    {
        _isGamePaused = false;
        Time.timeScale = 1f;
    }

    public void RestartGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Main Menu");
    }

    internal void RestartLevel()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
    }
}
