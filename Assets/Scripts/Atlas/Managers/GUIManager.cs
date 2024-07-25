using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class GUIManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _pausePanel;

    private void OnEnable()
    {
        GameManager.GameStateChange += HandleGameStateChange;
    }

    private void OnDisable()
    {
        GameManager.GameStateChange -= HandleGameStateChange;
    }

    private void HandleGameStateChange(GameState GameState)
    {
        switch (GameState)
        {
            case GameState.Paused:
                OnPause();
                break;
            case GameState.Live:
                OnLive();
                break;
        }
    }

    private void OnLive()
    {
        _pausePanel.SetActive(false);
    }
    private void OnPause()
    {
        _pausePanel.SetActive(true); 
    }


    public void RestartBtn()
    {
        GameManager.Instance.RestartLevel();
    }

    public void ResumeBtn()
    {
        GameManager.Instance.ResumeGame();
    }

}
