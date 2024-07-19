using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class CharacterMovement : CharacterAbility
{
    [SerializeField]
    private float _moveSpeed = 1f;
    [SerializeField]
    private float _turnTime = 0.2f;

    private bool _isMoving = false;
    private Cardinal _commandDirection;
    private Coroutine _movementCoroutine;

    private void OnValidate()
    {
        AbilityName = "Movement";
    }
    protected override void AbilityStart()
    {
        MoveCharacter(_commandDirection);
    }
   
    protected override void AbilityEnd()
    {
    }
    protected override void ProcessAbility()
    {
        if (Character.CharacterMode == CharacterType.Player)
        {
            ReadInput();            
        }      
    }

    private void ReadInput()
    {
        if (InputManager.Instance.Horizontal > 0f)
        {
            //If facing East, move East else face East.
            _commandDirection = Cardinal.East;
            if (IsAbilityPermitted)
            {
                AbilityStart();
            }
        }
        else if (InputManager.Instance.Horizontal < 0f)
        {
            //If facing West, move West else face West.
            _commandDirection = Cardinal.West;
            if (IsAbilityPermitted)
            {
                AbilityStart();
            }
        }
        else if (InputManager.Instance.Vertical > 0f)
        {
            //If facing North, move North else face North.
            _commandDirection = Cardinal.North;
            if (IsAbilityPermitted)
            {
                AbilityStart();
            }
        }
        else if (InputManager.Instance.Vertical < 0f)
        {
            //If facing South, move South else face South.
            _commandDirection = Cardinal.South;
            if (IsAbilityPermitted)
            {
                AbilityStart();
            }
        }
    }

    public void MoveCharacter(Cardinal commandDirection)
    {
        if (!_isMoving)
        {
            if (Character.FacingDirection != _commandDirection)
            {
                FaceDirection(_commandDirection);
            }
            else
            {
                Vector3 targetPos = transform.position + Character.CardinalToVect3(commandDirection);
                Vector2 rayDirection = Character.CardinalToVect3(commandDirection);
                //Debug.DrawRay(transform.position, rayDirection, Color.red, 2f);
                RaycastHit2D hit = Physics2D.Raycast(transform.position, rayDirection, 1f, LayerMask.GetMask("Obstacle"));
                if (!_isMoving && hit.collider == null)
                {
                    _movementCoroutine = StartCoroutine(MoveToPosition(targetPos));
                }
            }
        }        
    }

    private IEnumerator MoveToPosition(Vector3 target)
    {
        _isMoving = true;

        while ((target - transform.position).sqrMagnitude > Mathf.Epsilon)
        {            
            transform.position = Vector3.MoveTowards(transform.position, target, _moveSpeed * Time.deltaTime);
            yield return null;
        }
        transform.position = target;
        _isMoving = false;
    }
    public void FaceDirection(Cardinal dir)
    {
        Character.FacingDirection = dir;
        Vector3 faceDir = Character.GetDirectionScale(dir);

        StartCoroutine(TurnCharacter(faceDir));        

    }

    private IEnumerator TurnCharacter(Vector3 faceDir)
    {
        _isMoving = true;

        if (Character.CharacterModel != null)
        {
            Character.CharacterModel.transform.localScale = faceDir;
        }
        else
        {
            transform.localScale = faceDir;
        }
        
        yield return new WaitForSeconds(_turnTime);
        
        _isMoving = false;

    }

    protected override void UpdateCharacterAnimator()
    {
        Character.UpdateAnimator("Walking", _isMoving);
    }
}
