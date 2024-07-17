using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterPause : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if (InputManager.Instance.PauseButton)
        {
            GameManager.Instance.PauseGame();
        }
    }
}
