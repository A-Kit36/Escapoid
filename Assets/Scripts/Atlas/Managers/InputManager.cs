using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance { get; private set; }

    public float Horizontal { get; private set; }
    public float Vertical { get; private set; }

    public bool PauseButton { get; private set; }

    private void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    private void Update()
    {
        Horizontal = Input.GetAxis("Horizontal");
        Vertical = Input.GetAxis("Vertical");

        PauseButton = Input.GetButtonDown("Pause");
    }
}
