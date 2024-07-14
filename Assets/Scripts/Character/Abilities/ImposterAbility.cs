using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class ImposterAbility : CharAbility
{
    RuntimeAnimatorController enemyController;
    RuntimeAnimatorController storedController;
    PlayerAnimator playerAnimator;
    private bool inTriggerZone = false;
    [SerializeField] private float imposterTime;
    private bool timerStopped = false;
    [SerializeField] private TextMeshProUGUI timerUI; // temporary workaround before we have a UI manager
    private bool changeActivated;

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

    private void Update()
    {
        if (imposterTime > 0 && !timerStopped)
        {
            imposterTime -= Time.deltaTime;
        }
        timerUI.text = imposterTime.ToString();

        if (imposterTime <= 0)
        {
            playerAnimator.ChangeBack();
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
            imposterTime = 10f;
            changeActivated = true; // so the first time it HAS to happen while in trigger zone
        }
        else if (changeActivated && storedController != null)
        {
            playerAnimator.ChangeController(storedController);
            timerStopped = false;
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

    public void StopTimer()
    {
        if (changeActivated == true) // otherwise if you just randomly press this button the timer won't count down during transform
        {
            timerStopped = true;
        }
    }

}
