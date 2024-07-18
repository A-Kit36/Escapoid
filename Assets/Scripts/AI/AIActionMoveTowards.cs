using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIActionMoveTowards : AIAction
{
    [SerializeField]
    private float _moveSpeed = 5f;

    public override void Execute()
    {
        Brain.dummyCharacter1.UpdateAnimatorBool("IsMoving", true);
        Brain.dummyCharacter1.MoveTowardsTarget(Brain.Target, _moveSpeed);
    }

    public override void OnEnterState()
    {
        Brain.dummyCharacter1.UpdateAnimatorBool("IsMoving", true);
    }

    public override void OnExitState()
    {
        Brain.dummyCharacter1.UpdateAnimatorBool("IsMoving", false);
    }
}
