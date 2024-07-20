using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class VerifyCheck : MonoBehaviour, IVerify
{
    public bool FunctionChanged { get; private set; }
    [SerializeField] Role acceptedRole;

    public Role AcceptedRole
    {
        get { return acceptedRole; }
        set { acceptedRole = value; }
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            IRoleAssignable roleAssignable = other.GetComponent<IRoleAssignable>();
            Role playerRole = roleAssignable.UserRole;
            Debug.Log($"Player role is {playerRole}");

            if (Verify(playerRole) == true)
            {
                ChangeFromRole();
            }
        }
    }

    public bool Verify(Role retrievedRole)
    {
        if (retrievedRole == acceptedRole)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void ChangeFromRole()
    {
        FunctionChanged = true;
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        FunctionChanged = false; // this is needed so the behaviour changes if the player changes the role
    }
}
