using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance;
    public float Horizontal { get; private set; }
    public float Vertical { get; private set; }

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
        return Input.GetButtonDown("Imposter"); // button E
    }

    public bool GetTurnBackInput()
    {
        return Input.GetButtonDown("TurnBack"); // button Q
    }

    public bool GetInteractInput()
    {
        return Input.GetButtonDown("Interact"); // space button
    }
}
