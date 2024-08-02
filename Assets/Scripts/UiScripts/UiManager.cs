using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Unity.VisualScripting;

public class UiManager : MonoBehaviour
{
    //management of UiGameObjects (each UI is put in a list in the editor)
    public List<GameObject> uiList;
    private Dictionary<string, GameObject> uiDict;
    //management of MenuUIGameObjects (each UI is put in a list in the editor)
    public List<GameObject> inGameMenuList;
    private Dictionary<string, GameObject> inGameMenuDict;

    void Start()
    {
        //Create a collection with all the UI
        uiDict = new Dictionary<string, GameObject>
        {
            {"StartMenu", uiList[0]},
            {"MainHUD", uiList[1]},
            {"Pause", uiList[2]},
            {"GameOver", uiList[3]},
            {"InGameMenu", uiList[4]}, 
        };
        //Create a collection with all the InGameUI
        inGameMenuDict = new Dictionary<string, GameObject>
        {
            {"Status1", inGameMenuList[0]},
            {"Status2", inGameMenuList[1]},
            {"StoryScreen", inGameMenuList[2]},
            {"Dialogue1", inGameMenuList[3]},
            {"Dialogue2", inGameMenuList[4]}, 
            {"Options", inGameMenuList[5]}, 
            {"Credits", inGameMenuList[6]}, 
        };
        //Show the Main Menu UI
        StartMenu();
    }

    void Update()
    {
        
    }
    
    //StartMenu method
    public void StartMenu()
    {
        ActivateUI(uiDict["StartMenu"], uiList);
    }
    
    //Activate and Deactivate UI for only showing the wanted one at a time
    public void ActivateUI(GameObject uiToAct, List<GameObject> uiToDeac)
    {
        DeactivateUI(uiToDeac);
        uiToAct.SetActive(true);
    }
    public void DeactivateUI(List<GameObject> uiToDeac)
    {
        foreach (GameObject item in uiToDeac)
        {
            item.SetActive(false);
        }
    }
    
    //all the methods called by the ingame buttons 
    #region //AllStartMenuButtons
    public void NewGame()
    {
        //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        ActivateUI(uiDict["MainHUD"], uiList);
    }
    public void Continue()
    {
        
    }
    public void LoadSave()
    {
        
    }
    public void Options()
    {
        ActivateUI(uiDict["InGameMenu"], uiList);
        ActivateUI(inGameMenuDict["Options"], inGameMenuList);
    }
    public void Credits()
    {
        ActivateUI(uiDict["InGameMenu"], uiList);
        ActivateUI(inGameMenuDict["Credits"], inGameMenuList);
    }
    #endregion
    #region //AllInGameMenuUI 
    public void DialogueSolo()
    {
        ActivateUI(uiDict["InGameMenu"], uiList);
        ActivateUI(inGameMenuDict["Dialogue1"], inGameMenuList);
    }
    public void DialogueDuo()
    {
        ActivateUI(uiDict["InGameMenu"], uiList);
        ActivateUI(inGameMenuDict["Dialogue2"], inGameMenuList);
    }
    public void StoryScreen()
    {
        ActivateUI(uiDict["InGameMenu"], uiList);
        ActivateUI(inGameMenuDict["StoryScreen"], inGameMenuList);
    }
    public void Status1()
    {
        ActivateUI(uiDict["InGameMenu"], uiList);
        ActivateUI(inGameMenuDict["Status1"], inGameMenuList);
    }
    public void Status2()
    {
        ActivateUI(uiDict["InGameMenu"], uiList);
        ActivateUI(inGameMenuDict["Status2"], inGameMenuList);
    }
    #endregion
    
    //Pause and GameOver UIs
    public void Pause()
    {
        ActivateUI(uiDict["Pause"], uiList);
    }
    public void GameOver()
    {
        ActivateUI(uiDict["GameOver"], uiList);
    }


}
