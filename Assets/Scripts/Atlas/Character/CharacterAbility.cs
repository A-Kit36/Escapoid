using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CharacterAbility : MonoBehaviour
{
    public bool IsAbilityPermitted;

    private Character _character;

    private void Awake()
    {
        _character = GetComponentInParent<Character>();
    }

    private void Start()
    {
        InitializeAbility();
    }

    private void Update()
    {
        ProcessAbility();
    }

    protected virtual void InitializeAbility()
    {
        IsAbilityPermitted = true;
    }
    protected abstract void AbilityStart();
    protected abstract void ProcessAbility();
    protected abstract void AbilityEnd();
    


}
