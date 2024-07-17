using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIActionDoNothing : AIAction
{
    public override void Execute()
    {
        Brain.Character.UpdateAnimatorBool("Idle", true);
    }

    public override void OnEnterState()
    {
        Brain.Character.UpdateAnimatorBool("Idle", true);
    }

    public override void OnExitState()
    {
        Brain.Character.UpdateAnimatorBool("Idle", false);
    }
}
