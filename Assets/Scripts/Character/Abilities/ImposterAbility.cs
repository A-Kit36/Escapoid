using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ImposterAbility : CharAbility
{
    RuntimeAnimatorController enemyController;
    RuntimeAnimatorController storedController;
    PlayerAnimator playerAnimator;
    private bool inTriggerZone = false;

    private bool isActive = true;

    public override bool IsActive
    {
        get { return isActive; }
        set { isActive = value; }
    }

    private void Awake()
    {
        playerAnimator = GetComponent<PlayerAnimator>();
    }
    public override void Trigger()
    {
        if (!isActive)
        {
            return;
        }

        if (inTriggerZone)
        {
            playerAnimator.ChangeSkin();
            playerAnimator.ChangeController(storedController);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            Animator enemyAnimator = other.GetComponent<Animator>();
            enemyController = enemyAnimator.runtimeAnimatorController; //getting the controller the enemy animator uses
            inTriggerZone = true;
            Imposter(enemyController); // storing this so the player can potentially store it as well
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        inTriggerZone = false;
    }

    private void Imposter(RuntimeAnimatorController runtimeAnimatorController)
    {
        storedController = runtimeAnimatorController;
        Debug.Log("Controller Stored");
    }

}
