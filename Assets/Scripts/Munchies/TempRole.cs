using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempRole : MonoBehaviour, IRoleAssignable
{
    [SerializeField] Role userRole;

    public Role UserRole
    {
        get { return userRole; }

        set { userRole = value; }
    }
}
