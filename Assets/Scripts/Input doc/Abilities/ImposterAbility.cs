using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class ImposterAbility : CharAbility
{
    RuntimeAnimatorController enemyController;
    RuntimeAnimatorController storedController;
    BoxCollider2D storedCollider;
    BoxCollider2D OGcollider; // we need to store it so we can revert to it later
    BoxCollider2D runtimeCollider;
    Role storedRole;
    PlayerAnimator playerAnimator;
    RoleController roleController;
    private bool inTriggerZone = false;
    [SerializeField] float ImposterTimeSetting;
    private float imposterTime;
    private bool timerStopped = false;
    //[SerializeField] private TextMeshProUGUI timerUI; // temporary workaround before we have a UI manager
    private bool changeActivated;
    public bool IsImposter { get; private set; } // this is necessary for the TurnBackAbility script - it needs to monitor when we are in the fake form

    private void Awake()
    {
        playerAnimator = GetComponent<PlayerAnimator>();
        roleController = GetComponent<RoleController>();
        OGcollider = GetComponent<BoxCollider2D>();
        //ogScale = transform.localScale;
    }

    private void Update()
    {
        if (imposterTime > 0 && !timerStopped)
        {
            imposterTime -= Time.deltaTime;
        }
        UiManager.Instance.timer.text = imposterTime.ToString("F1");

        if (imposterTime <= 0 && changeActivated)
        {
            playerAnimator.ChangeBack();
            roleController.SetOGRole();
            RestoreCollider();
            IsImposter = false;
            changeActivated = false;
        }

        if (DialogueBoxController.Instance.dialogueActive)
        {
            timerStopped = true;
        }
        else if (IsImposter)
        {
            timerStopped = false;
        }
    }
    public override void Trigger()
    {
        if (!IsActive)
        {
            return;
        }

        if (inTriggerZone && !changeActivated)
        {
            PerformChange();
            imposterTime = ImposterTimeSetting;
            changeActivated = true; // so the first time it HAS to happen while in trigger zone
        }
        else if (changeActivated && storedController != null)
        {
            PerformChange();
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
            //Debug.Log($"Enemy role is {enemyRole}");

            BoxCollider2D enemyCollider = FindChildCollider(other); // otherwise it gets the wrong collider

            inTriggerZone = true;
            Imposter(enemyController, enemyRole, enemyCollider); // storing this so the player can potentially store it as well

        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        inTriggerZone = false;
    }

    private void Imposter(RuntimeAnimatorController runtimeAnimatorController, Role role, BoxCollider2D boxCollider2D)
    {
        storedController = runtimeAnimatorController;
        storedRole = role;
        storedCollider = boxCollider2D;
        //Debug.Log("Controller Stored");
    }

    public void StopTimer()
    {
        if (changeActivated == true) // otherwise if you just randomly press this button the timer won't count down during transform
        {
            timerStopped = true;
            IsImposter = false;
            RestoreCollider();
            roleController.SetOGRole();
        }
    }

    private void SwapColliders(BoxCollider2D storedCollider)
    {
        OGcollider.enabled = false;
        BoxCollider2D newBox = gameObject.AddComponent<BoxCollider2D>();
        newBox.size = storedCollider.size;
        newBox.offset = storedCollider.offset;
        runtimeCollider = newBox;
        runtimeCollider.enabled = true;
    }

    private void RestoreCollider()
    {
        Destroy(runtimeCollider);
        OGcollider.enabled = true;
    }

    private BoxCollider2D FindChildCollider(Collider2D enemyCollider)
    {
        BoxCollider2D[] colliders = enemyCollider.GetComponentsInChildren<BoxCollider2D>();
        foreach (BoxCollider2D col in colliders)
        {
            if (col.gameObject != enemyCollider.gameObject) // excluding the collider on the main gameobject
            {
                return col;
            }
        }
        return null;
    }

    private void PerformChange()
    {
        playerAnimator.ChangeSkin(storedController);
        //yield return new WaitForSeconds(0.7f);
        //playerAnimator.ChangeController(storedController);
        roleController.ChangeRole(storedRole);
        SwapColliders(storedCollider);
        IsImposter = true;
        //yield return null;
    }

}
