using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoleController : MonoBehaviour, IRoleAssignable
{
    [SerializeField] private Role userRole;

    public Role UserRole
    {
        get { return userRole; }
        set { userRole = value; }
    }

    public void ChangeRole(Role role)
    {
        UserRole = role;
    }

    public void SetOGRole()
    {
        UserRole = Role.MainCharacter;
    }
}
