using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class VerifyAbility : MonoBehaviour
{
    public virtual bool InRange { get; private set; }
    VerifyCheck verifyCheck;
    public virtual void Start()
    {
        verifyCheck = GetComponent<VerifyCheck>();
    }

    public virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (verifyCheck.FunctionChanged)
        {
            NewFunction();
        }
        else
        {
            OldFunction();
        }
        InRange = true;
    }

    public virtual void OnTriggerExit2D(Collider2D other)
    {
        InRange = false;
    }

    public abstract void OldFunction();
    public abstract void NewFunction();
}
