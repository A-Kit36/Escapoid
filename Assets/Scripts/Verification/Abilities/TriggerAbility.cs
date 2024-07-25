using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TriggerAbility : VerifyCheck
{
    // This abstract class is for items that get triggered when you enter a trigger zone. You just inherit from this script where you write the main logic of the item
    public override void Verify(Role retrievedRole)
    {
        if (retrievedRole == AcceptedRole)
        {
            NewFunction();
        }
        else
        {
            OldFunction();
        }
    }

    public override abstract void OldFunction();
    public override abstract void NewFunction();
}
