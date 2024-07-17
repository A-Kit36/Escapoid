using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterPause : CharacterAbility
{

    protected override void AbilityStart()
    {
        GameManager.Instance.PauseGame();
    }

    protected override void ProcessAbility()
    {
        if (InputManager.Instance.PauseButton)
        {
            if (GameManager.Instance.IsGamePaused)
            {
                AbilityEnd();
            }
            else
            {
                AbilityStart();
            }
        }
    }
    protected override void AbilityEnd()
    {
        GameManager.Instance.ResumeGame();
    }
}
