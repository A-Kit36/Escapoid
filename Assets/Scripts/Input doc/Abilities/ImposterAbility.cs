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
    Vector3 ogScale;
    Vector3 storedScale;
    PlayerAnimator playerAnimator;
    RoleController roleController;
    private bool inTriggerZone = false;
    SpriteRenderer ownspriteRenderer;
    [SerializeField] float ImposterTimeSetting;
    private float imposterTime;
    private bool timerStopped = false;
    //[SerializeField] private TextMeshProUGUI timerUI; // temporary workaround before we have a UI manager
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
        OGcollider = GetComponent<BoxCollider2D>();
        ownspriteRenderer = GetComponent<SpriteRenderer>();
        //ogScale = transform.localScale;
    }

    private void Update()
    {
        if (imposterTime > 0 && !timerStopped)
        {
            imposterTime -= Time.deltaTime;
        }
        //timerUI.text = imposterTime.ToString();

        if (imposterTime <= 0 && changeActivated)
        {
            playerAnimator.ChangeBack();
            roleController.SetOGRole();
            RestoreCollider();
            ChangeSize();
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

            SpriteRenderer enemyspriteRenderer = other.gameObject.GetComponent<SpriteRenderer>();
            Vector2 enemyspriteSize = enemyspriteRenderer.bounds.size;
            Vector2 ownspriteSize = ownspriteRenderer.bounds.size;

            // calculating the scale factor between our size and enemy's size
            Vector3 scaleFactor = new Vector3(enemyspriteSize.x / ownspriteSize.x, enemyspriteSize.y / ownspriteSize.y, 1.0f);

            inTriggerZone = true;
            Imposter(enemyController, enemyRole, enemyCollider, scaleFactor); // storing this so the player can potentially store it as well

        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        inTriggerZone = false;
    }

    private void Imposter(RuntimeAnimatorController runtimeAnimatorController, Role role, BoxCollider2D boxCollider2D, Vector3 scale)
    {
        storedController = runtimeAnimatorController;
        storedRole = role;
        storedCollider = boxCollider2D;
        storedScale = scale;
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
            ChangeSize();
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

    private void ChangeSize()
    {
        transform.localScale = storedScale;
        //Debug.Log("Scale Changed");
    }

    private void PerformChange()
    {
        playerAnimator.ChangeSkin();
        playerAnimator.ChangeController(storedController);
        roleController.ChangeRole(storedRole);
        SwapColliders(storedCollider);
        ChangeSize();
        IsImposter = true;
    }

}
