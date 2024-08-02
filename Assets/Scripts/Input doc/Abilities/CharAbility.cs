using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CharAbility : MonoBehaviour
{
    [SerializeField] bool isActive = true;

    public virtual bool IsActive
    {
        get { return isActive; }
        set { isActive = value; }
    }
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
