using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CharacterAbility : MonoBehaviour
{
    public bool IsAbilityPermitted = true;

    [SerializeField]    
    public string AbilityName { get; private set; }

    [NonSerialized]
    public Character Character;

    private void Awake()
    {
        Character = GetComponentInParent<Character>();
    }

    private void Start()
    {
        InitializeAbility();
    }

    private void Update()
    {
        ProcessAbility();
        UpdateCharacterAnimator();
    }

    protected virtual void InitializeAbility()
    {
        IsAbilityPermitted = true;
    }
    protected abstract void AbilityStart();
    protected abstract void ProcessAbility();
    protected abstract void AbilityEnd();
    protected abstract void UpdateCharacterAnimator();
    


}
