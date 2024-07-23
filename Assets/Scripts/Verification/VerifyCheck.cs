using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class VerifyCheck : MonoBehaviour, IVerify
{
    // This is the daddy of all other classes that will relate to the abilities of items. 
    // That is the class that is the main blueprint for setting an accepted role and checking if the accepted role is fulfilled. 
    // Two more abstract classes derive from it: InteractAbility and TriggerAbility - one for items where you need to press Space to interact, and another for items that work when triggered
    public virtual bool InRange { get; private set; }
    private bool functionChanged;
    public bool FunctionChanged
    {
        get { return functionChanged; }
        set { functionChanged = value; }
    }
    [SerializeField] Role acceptedRole;

    public Role AcceptedRole
    {
        get { return acceptedRole; }
        set { acceptedRole = value; }
    }

    public virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            IRoleAssignable roleAssignable = other.GetComponent<IRoleAssignable>();
            Role playerRole = roleAssignable.UserRole;
            Debug.Log($"Player role is {playerRole}");
            Verify(playerRole);
            InRange = true;
        }

    }

    public virtual void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            IRoleAssignable roleAssignable = other.GetComponent<IRoleAssignable>();
            Role playerRole = roleAssignable.UserRole;
            Verify(playerRole);
        }
    }
    public virtual void OnTriggerExit2D(Collider2D other)
    {
        InRange = false;
    }

    public abstract void Verify(Role retrievedRole);
    public abstract void OldFunction();
    public abstract void NewFunction();
}
