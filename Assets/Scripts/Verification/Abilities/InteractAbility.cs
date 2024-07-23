using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InteractAbility : VerifyCheck
{
    // This abstract class is for items that you need to interact with. You just inherit from this script where you write the main logic of the item

    public virtual void Update()
    {
        if (InputManager.Instance.GetInteractInput() && InRange)
        {
            if (FunctionChanged)
            {
                NewFunction();
            }
            else
            {
                OldFunction();
            }
        }
    }

    public override void Verify(Role retrievedRole)
    {
        if (retrievedRole == AcceptedRole)
        {
            FunctionChanged = true;
        }
        else
        {
            FunctionChanged = false;
        }
    }

    public override abstract void OldFunction();
    public override abstract void NewFunction();
}
