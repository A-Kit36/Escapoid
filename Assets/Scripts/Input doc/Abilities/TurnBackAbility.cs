using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnBackAbility : CharAbility
{
    PlayerAnimator playerAnimator;
    ImposterAbility imposterAbility;


    private void Awake()
    {
        playerAnimator = GetComponent<PlayerAnimator>();
        imposterAbility = GetComponent<ImposterAbility>();
    }
    public override void Trigger()
    {
        if (!IsActive)
        {
            return;
        }

        if (imposterAbility.IsImposter)
        {
            playerAnimator.ChangeBack();
            imposterAbility.StopTimer();
        }
    }
}
