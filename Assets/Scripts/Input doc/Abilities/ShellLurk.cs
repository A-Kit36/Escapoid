using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShellLurk : CharAbility
{
    PlayerMovement playerMovement;
    PlayerAnimator playerAnimator;
    Rigidbody2D rb;

    private bool movingShell = false;
    private bool inShell = false;

    [SerializeField] float shellSpeed;

    private void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();
        playerAnimator = GetComponent<PlayerAnimator>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (InputManagerOption.Instance.GetExtraAbilityInput())
        {
            Trigger();
        }
    }

    private void FixedUpdate()
    {
        if (movingShell)
        {
            HandleShellMovement();
        }
    }

    public override void Trigger()
    {
        if (!IsActive)
        {
            return;
        }

        if (!inShell)
        {
            HardenShell();
        }
        else
        {
            UnShell();
        }

    }

    private void HardenShell()
    {
        inShell = true;
        playerMovement.DisableMovenent();
        playerAnimator.GoIntoShell();
        EnableShellMove();
        Debug.Log("Invincible to damage");
    }

    private void UnShell()
    {
        inShell = false;
        DisableShellMove();
        playerAnimator.UnShell();
        playerMovement.EnableMovenent();
        Debug.Log("Vulnerable to damage");
    }

    private void HandleShellMovement()
    {

    }

    private void EnableShellMove()
    {
        movingShell = true;
    }

    private void DisableShellMove()
    {
        movingShell = false;
    }

    public override void Deactivate()
    {
        base.Deactivate();
        UnShell();
    }
}
