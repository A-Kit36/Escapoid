using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class ImposterAbility : CharAbility
{
    RuntimeAnimatorController enemyController;
    RuntimeAnimatorController storedController;
    Role storedRole;
    PlayerAnimator playerAnimator;
    RoleController roleController;
    private bool inTriggerZone = false;
    [SerializeField] private float imposterTime;
    private bool timerStopped = false;
    [SerializeField] private TextMeshProUGUI timerUI; // temporary workaround before we have a UI manager
    private bool changeActivated;
    public bool IsImposter { get; private set; } // this is necessary for the TurnBackAbility script - it needs to monitor when we are in the fake form

    private bool isActive = true;

    public override bool IsActive
    {
        get { return isActive; }
        set { isActive = value; }
    }

    private void Awake()
    {
        playerAnimator = GetComponent<PlayerAnimator>();
        roleController = GetComponent<RoleController>();
    }

    private void Update()
    {
        if (imposterTime > 0 && !timerStopped)
        {
            imposterTime -= Time.deltaTime;
        }
        timerUI.text = imposterTime.ToString();

        if (imposterTime <= 0 && changeActivated)
        {
            playerAnimator.ChangeBack();
            roleController.SetOGRole();
            IsImposter = false;
            changeActivated = false;
        }
    }
    public override void Trigger()
    {
        if (!isActive)
        {
            return;
        }

        if (inTriggerZone && !changeActivated)
        {
            playerAnimator.ChangeSkin();
            playerAnimator.ChangeController(storedController);
            roleController.ChangeRole(storedRole);
            imposterTime = 10f;
            IsImposter = true;
            changeActivated = true; // so the first time it HAS to happen while in trigger zone
        }
        else if (changeActivated && storedController != null)
        {
            playerAnimator.ChangeController(storedController);
            roleController.ChangeRole(storedRole);
            IsImposter = true;
            timerStopped = false;
        }

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            Animator enemyAnimator = other.GetComponent<Animator>();
            enemyController = enemyAnimator.runtimeAnimatorController; //getting the controller the enemy animator uses

            IRoleAssignable roleAssignable = other.GetComponent<IRoleAssignable>();
            Role enemyRole = roleAssignable.UserRole;
            Debug.Log($"Enemy role is {enemyRole}");

            inTriggerZone = true;
            Imposter(enemyController, enemyRole); // storing this so the player can potentially store it as well
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        inTriggerZone = false;
    }

    private void Imposter(RuntimeAnimatorController runtimeAnimatorController, Role role)
    {
        storedController = runtimeAnimatorController;
        storedRole = role;
        Debug.Log("Controller Stored");
    }

    public void StopTimer()
    {
        if (changeActivated == true) // otherwise if you just randomly press this button the timer won't count down during transform
        {
            timerStopped = true;
            IsImposter = false;
            roleController.SetOGRole();
        }
    }

}
