using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIActionDoNothing : AIAction
{
    public override void Execute()
    {
        Brain.dummyCharacter1.UpdateAnimatorBool("Idle", true);
    }

    public override void OnEnterState()
    {
        Brain.dummyCharacter1.UpdateAnimatorBool("Idle", true);
    }

    public override void OnExitState()
    {
        Brain.dummyCharacter1.UpdateAnimatorBool("Idle", false);
    }
}
