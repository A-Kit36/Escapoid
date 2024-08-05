using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManagerOption : MonoBehaviour
{
    public static InputManagerOption Instance;
    public float Horizontal { get; private set; }
    public float Vertical { get; private set; }
    private bool buttonsDisabled = false;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

    }

    private void Update()
    {
        Horizontal = Input.GetAxis("Horizontal");
        Vertical = Input.GetAxis("Vertical");
    }
    public float GetHorizontalInput()
    {
        return Input.GetAxisRaw("Horizontal");
    }

    public float GetVerticalInput()
    {
        return Input.GetAxisRaw("Vertical");
    }

    public bool GetImposterInput()
    {
        if (buttonsDisabled)
        {
            return false;
        }
        return Input.GetButtonDown("Imposter"); // button E
    }

    public bool GetTurnBackInput()
    {
        if (buttonsDisabled)
        {
            return false;
        }
        return Input.GetButtonDown("TurnBack"); // button Q
    }

    public bool GetInteractInput()
    {
        if (buttonsDisabled)
        {
            return false;
        }
        return Input.GetButtonDown("Interact"); // space button
    }

    public bool GetExtraAbilityInput()
    {
        if (buttonsDisabled)
        {
            return false;
        }
        return Input.GetButtonDown("ExtraAbility"); // R button
    }

    public bool GetEscapeInput()
    {
        return Input.GetButtonDown("Pause");
    }

    public void DisableAllButtons()
    {
        buttonsDisabled = true;
    }
    public void EnableAllButtons()
    {
        buttonsDisabled = false;
    }
}
