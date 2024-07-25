using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum CharacterType { Player, AI }
public enum Cardinal { North, South, East, West }

public class Character : MonoBehaviour
{    
    public GameObject CharacterModel;
    public CharacterType CharacterMode = CharacterType.Player;
    public AIBrain Brain;
    public Cardinal FacingDirection = Cardinal.South;

    private Animator _animator;
    private Rigidbody2D _rigidbody2D;
    
    public List<CharacterAbility> _abilities;

    private void Awake()
    {
        if (CharacterModel != null) 
        { 
            _animator = CharacterModel.GetComponent<Animator>();
            _abilities = GetComponentsInChildren<CharacterAbility>().ToList();
        }
        else
        {
            _animator = GetComponent<Animator>();
        }

        _rigidbody2D = GetComponent<Rigidbody2D>();        

        if(CharacterMode == CharacterType.AI)
        {
            if (Brain != null) 
            { 
                Brain.SetCharacter(this);
            }
        }
    }
    public Vector3 CardinalToVect3(Cardinal dir)
    {
        switch (dir)
        {
            case Cardinal.North:
                return Vector2.up;
            case Cardinal.South:
                return Vector2.down;
            case Cardinal.West:
                return Vector2.left;
            case Cardinal.East:
                return Vector2.right;
            default:
                return Vector2.down;
        }
    }
    public Vector3 GetDirectionScale(Cardinal dir)
    {
        switch (dir)
        {
            case Cardinal.North:
                return Vector2.one;
            case Cardinal.South:
                return Vector2.one;
            case Cardinal.West:
                return Vector2.left + Vector2.up;
            case Cardinal.East:
                return Vector2.right + Vector2.up;
            default:
                return Vector2.down;
        }
    }

    public void UpdateAnimator(string name, bool value)
    {
        if (_animator != null) 
        {
            _animator.SetBool(name, value);
        }
    }

    public void UpdateAnimator(string name, float value)
    {
        if (_animator != null)
        {
            _animator.SetFloat(name, value);
        }
    }
    public void UpdateAnimator(string triggerName)
    {
        if (_animator != null)
        {
            _animator.SetTrigger(name);
        }
    }
}
