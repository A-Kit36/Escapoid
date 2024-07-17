using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class GUIManager : MonoBehaviour
{
    public static GUIManager Instance { get; private set; }

    [SerializeField]
    private GameObject _pausePanel;

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

    public void OnPause()
    {
        _pausePanel.SetActive(true);
    }
    public void OnResume()
    {
        GameManager.Instance.ResumeGame();
        _pausePanel.SetActive(false);
    }
    public void OnRestart()
    {
        GameManager.Instance.RestartLevel();
    }

}
