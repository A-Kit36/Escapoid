using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Unity.VisualScripting;

public class UiManager : MonoBehaviour
{
    public GameObject startMenu;
    public GameObject pauseMenu;
    public GameObject gameOver;
    public GameObject dialogue1;
    public GameObject dialogue2;
    public GameObject menuHUD1;
    public GameObject menuHUD2;
    public GameObject storyScreen;
    public GameObject playHUD;
    
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void NewGame()
    {
        //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        startMenu.SetActive(false);
    } 
    public void Continue()
    {
        startMenu.SetActive(false);
    }
    public void LoadSave()
    {
        startMenu.SetActive(false);
    }
    public void Options()
    {
        startMenu.SetActive(false);
    }
    public void Credits()
    {
        startMenu.SetActive(false);
    }
    public void StartMenu()
    {
        startMenu.SetActive(true);
    }
    public void Pause()
    {

    }
    public void GameOver()
    {
        
    }
    public void MainHUD()
    {

    }
    public void DialogueSolo()
    {

    }
    public void DialogueDuo()
    {

    }
    public void StoryScreen()
    {

    }
    public void InGameMenu()
    {

    }
    
    
}
