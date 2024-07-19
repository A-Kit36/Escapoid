using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIActionMoveTowards : AIAction
{
    [SerializeField]
    private float _moveSpeed = 5f;

    private CharacterMovement _charMovement;

    public override void Execute()
    {
        //calculate next move
        //send move command w/ cardinal
        _charMovement.MoveCharacter(Cardinal.North);
    }

    public override void OnEnterState()
    {
        _charMovement = (CharacterMovement)Brain.Character._abilities.Find(p => p.AbilityName == "Movement");
        if (_charMovement == null) 
        {
            Debug.Log("AI Agent needs CharacterMovement to utilize AIActionMoveTowards.");
        }
    }

    public override void OnExitState()
    {
    }
}
