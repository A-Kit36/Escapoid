using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public enum CharacterType { Player, AI}

    public GameObject CharacterModel;
    public CharacterType CharacterMode = CharacterType.Player;
    public AIBrain Brain;

    private Animator _animator;
    private Rigidbody2D _rigidbody2D;
    //private CharacterAbilitiy[] _abilities;

    private void Awake()
    {
        if (CharacterModel != null) 
        { 
            _animator = CharacterModel.GetComponent<Animator>();
            //_abilities = GetComponentsInChildren<CharacterAbility>();
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

    //this will be done through the CharacterMovement script
    public void MoveTowardsTarget(Transform target, float speed)
    {
        float step = speed * Time.deltaTime;
        transform.position = Vector2.MoveTowards(transform.position, target.position, step);
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
