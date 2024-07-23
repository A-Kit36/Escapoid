using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIActionPatrol : AIAction
{
    enum PatrolType { ForwardBack, Loop }

    [SerializeField]
    private Transform[] _pivotSpots;
    [SerializeField]
    private PatrolType _patrolType = PatrolType.Loop;

    private int _index = 0;
    private bool _isForward = true;
    private Transform _currentPivot;
    private float _distanceToTarget;
    private NPCMovement _charMovement;

    public override void Execute()
    {
        Transform charTransform = Brain.Character.transform;

        if (_currentPivot.position.x > charTransform.position.x)
        {
            _charMovement.MoveCharacter(Cardinal.East);
        }
        else if (_currentPivot.position.x < charTransform.position.x)
        {
            _charMovement.MoveCharacter(Cardinal.West);
        }
        else if (_currentPivot.position.y > charTransform.position.y)
        {
            _charMovement.MoveCharacter(Cardinal.North);
        }
        else
        {
            _charMovement.MoveCharacter(Cardinal.South);
        }

        _distanceToTarget = Vector2.Distance(transform.position, _currentPivot.position);

        if (_distanceToTarget <= 0.3f)
        {
            switch (_patrolType)
            {
                case PatrolType.ForwardBack:
                    HandleForwardBackPat();
                    break;
                case PatrolType.Loop:
                    HandleLoopPat();
                    break;
                default:
                    break;
            }
        }
    }

    private void HandleLoopPat()
    {
        _index++;
        if (_index < _pivotSpots.Length)
        {
            _currentPivot = _pivotSpots[_index];
        }
        else
        {
            _index = 0;
            _currentPivot = _pivotSpots[_index];
        }
    }

    private void HandleForwardBackPat()
    {
        if (_isForward)
        {
            _index++;
            if (_index < _pivotSpots.Length)
            {
                _currentPivot = _pivotSpots[_index];
            }
            else
            {
                _index -= 2;
                _isForward = false;
                _currentPivot = _pivotSpots[_index];
            }
        }
        else
        {
            _index--;
            if (_index >= 0)
            {
                _currentPivot = _pivotSpots[_index];
            }
            else
            {
                _index += 2;
                _isForward = true;
                _currentPivot = _pivotSpots[_index];
            }
        }
    }

    public override void OnEnterState()
    {
        _charMovement = (NPCMovement)Brain.Character._abilities.Find(p => p.AbilityName == "Movement");
        _currentPivot = _pivotSpots[_index];
        _distanceToTarget = Vector2.Distance(transform.position, _currentPivot.position);


    }

    public override void OnExitState()
    {

    }
}