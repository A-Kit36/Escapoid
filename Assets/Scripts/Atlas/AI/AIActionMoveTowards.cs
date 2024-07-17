using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIActionMoveTowards : AIAction
{
    [SerializeField]
    private float _moveSpeed = 5f;

    public override void Execute()
    {
        Brain.Character.UpdateAnimatorBool("Walking", true);
        Brain.Character.MoveTowardsTarget(Brain.Target, _moveSpeed);
    }

    public override void OnEnterState()
    {
        Brain.Character.UpdateAnimatorBool("Walking", true);
    }

    public override void OnExitState()
    {
        Brain.Character.UpdateAnimatorBool("Walking", false);
    }
}
