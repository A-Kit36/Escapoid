using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIActionDoNothing : AIAction
{
    public override void Execute()
    {
        Brain.Character.UpdateAnimator("Idle", true);
    }

    public override void OnEnterState()
    {
        Brain.Character.UpdateAnimator("Idle", true);
    }

    public override void OnExitState()
    {
        Brain.Character.UpdateAnimator("Idle", false);
    }
}
