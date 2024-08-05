using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public float AwarenessLevel; //Capitalize public members i.e. AwarenessLevel this belongs in a CharacterAbility Script

    public delegate void GameStateChanged(GameState GameState);
    public static event GameStateChanged GameStateChange;
    private bool isPaused;
    public bool FirstStoryRead { get; private set; }
    public bool SecondStoryP1Read { get; private set; }
    public bool SecondStoryP2Read { get; private set; }
    public bool ThirdStoryRead { get; private set; }

    public bool IsGamePaused { get; private set; }
    [SerializeField] int retries;
    public int Retries
    {
        get { return retries; }
        set { retries = value; }
    }

    private bool gameOverroutine;

    [SerializeField]
    private int _maxLives;
    [SerializeField]
    private int _livesLeft;

    [SerializeField] private CanvasGroup canvasBlack;
    //[SerializeField] float TimeToFade = 0.5f;
    [SerializeField] AudioClip gameOverSound;


    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    void Start()
    {
        Retries = 0;
        AwarenessLevel = 0f;
        _livesLeft = 3;
    }

    private void Update()
    {
        if (InputManagerOption.Instance.GetEscapeInput())
        {
            if (!isPaused)
            {
                isPaused = true;
                InputManagerOption.Instance.DisableAllButtons();
                UiManager.Instance.Pause();
            }
            else
            {
                isPaused = false;
                InputManagerOption.Instance.EnableAllButtons();
                UiManager.Instance.Resume();
            }
        }
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

    public IEnumerator GameOver()
    {
        gameOverroutine = true;
        /* while (canvasBlack.alpha < 1)
        {
            canvasBlack.alpha += TimeToFade * Time.deltaTime;
            yield return null;
        } */
        SoundManager.Instance.StopMusic();
        LevelManager.Instance.GameOverScreen();
        AudioPoolManager.Instance.PlayAudioClip(gameOverSound);
        yield return new WaitForSeconds(2);
        gameOverroutine = false;
        RestartLevel();
        Debug.Log("Game Over!");
        /* GameStateChange(GameState.GameOver);
        //add a reload screen to beginning of game */
    }

    public void PauseGame()
    {
        IsGamePaused = true;
        Time.timeScale = 0f;
        GameStateChange(GameState.Paused);
    }

    public void ResumeGame()
    {
        IsGamePaused = false;
        Time.timeScale = 1f;
        GameStateChange(GameState.Live);
    }

    public void RestartGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("StartMenu");
    }

    internal void RestartLevel()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
        Retries++;
    }

    public void NextLevel()
    {
        Retries = 0;
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void HandleGameOver()
    {
        if (gameOverroutine)
        {
            return;
        }
        StartCoroutine(GameOver());
    }

    public void HandleGameEnd()
    {
        RestartGame();
    }

    public void ReadLog(DialogueNumber dialogueNumber)
    {
        switch (dialogueNumber)
        {
            case DialogueNumber.FirstStory:
                FirstStoryRead = true;
                break;
            case DialogueNumber.SecondStoryP1:
                SecondStoryP1Read = true;
                break;
            case DialogueNumber.SecondStoryP2:
                SecondStoryP2Read = true;
                break;
            case DialogueNumber.ThirdStory:
                ThirdStoryRead = true;
                break;
        }
    }
}

public enum GameState
{
    Paused,
    Live,
    GameOver
}