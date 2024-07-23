using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIActionMoveTowards : AIAction
{
    private NPCMovement _charMovement;

    public override void Execute()
    {
        Transform targetTransform = Brain.Target.transform;
        Transform charTransform = Brain.Character.transform;

        if (targetTransform.position.x > charTransform.position.x)
        {
            _charMovement.MoveCharacter(Cardinal.East);
        }
        else if (targetTransform.position.x < charTransform.position.x)
        {
            _charMovement.MoveCharacter(Cardinal.West);
        }
        else if (targetTransform.position.y > charTransform.position.y)
        {
            _charMovement.MoveCharacter(Cardinal.North);
        }
        else
        {
            _charMovement.MoveCharacter(Cardinal.South);
        }
    }

    public override void OnEnterState()
    {
        _charMovement = (NPCMovement)Brain.Character._abilities.Find(p => p.AbilityName == "Movement");
        if (_charMovement == null)
        {
            Debug.Log("AI Agent needs NPCMovement to utilize AIActionMoveTowards.");
        }
    }

    public override void OnExitState()
    {
    }
}

