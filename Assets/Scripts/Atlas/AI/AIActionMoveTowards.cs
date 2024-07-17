using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIActionMoveTowards : AIAction
{
    [SerializeField]
    private float _moveSpeed = 5f;

    public override void Execute()
    {
        Brain.Character.UpdateAnimator("Walking", true);
        Brain.Character.MoveTowardsTarget(Brain.Target, _moveSpeed);
    }

    public override void OnEnterState()
    {
        Brain.Character.UpdateAnimator("Walking", true);
    }

    public override void OnExitState()
    {
        Brain.Character.UpdateAnimator("Walking", false);
    }
}
