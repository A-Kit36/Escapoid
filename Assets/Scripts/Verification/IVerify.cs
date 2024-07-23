using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IVerify
{
    Role AcceptedRole { get; set; }
    bool FunctionChanged { get; set; }
    void OnTriggerEnter2D(Collider2D other);
    void OnTriggerExit2D(Collider2D other);
    void Verify(Role role);

}
