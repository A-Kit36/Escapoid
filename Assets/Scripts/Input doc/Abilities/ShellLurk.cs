using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShellLurk : CharAbility
{
    PlayerMovement playerMovement;
    PlayerAnimator playerAnimator;
    Rigidbody2D rb;
    private Vector2 movementInput;
    private bool isMoving = false; // this bool is for checking if we are moving while in shell form
    [SerializeField] private bool movingShell = false; // this bool is for enabling shell-based movement(from collision to collision)
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
        isMoving = false;
        inShell = false;
        DisableShellMove();
        playerAnimator.UnShell();
        playerMovement.EnableMovenent();
        Debug.Log("Vulnerable to damage");
    }

    private void HandleShellMovement()
    {
        movementInput.x = InputManagerOption.Instance.GetHorizontalInput();
        movementInput.y = InputManagerOption.Instance.GetVerticalInput();

        // removing diagonal movement
        if (movementInput.x != 0)
        {
            movementInput.y = 0;
        }

        if (!isMoving)
        {
            rb.velocity = movementInput * shellSpeed;
            isMoving = true;
        }

    }

    private void OnCollisionEnter2D(Collision2D other)
    {

        rb.velocity = Vector2.zero;
        DisableShellMove();


    }

    private void EnableShellMove()
    {
        movingShell = true;
    }

    private void DisableShellMove()
    {
        rb.velocity = Vector2.zero;
        movingShell = false;
        isMoving = false;
    }

    public override void Deactivate()
    {
        base.Deactivate();
        UnShell();
    }
}
