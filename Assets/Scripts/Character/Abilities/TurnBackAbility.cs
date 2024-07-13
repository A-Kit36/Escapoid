using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnBackAbility : CharAbility
{
    PlayerAnimator playerAnimator;
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
        playerAnimator.ChangeBack();
    }
}
