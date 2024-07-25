using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CharAbility : MonoBehaviour
{
    public abstract bool IsActive { get; set; }
    public abstract void Trigger();

    public virtual void Activate()
    {
        Debug.Log("Ability activated");
        IsActive = true;
    }
    public virtual void Deactivate()
    {
        Debug.Log("Ability deactivated");
        IsActive = false;
    }
}
