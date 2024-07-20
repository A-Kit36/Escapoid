using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpen : VerifyAbility
{
    SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }


    public override void OldFunction()
    {
        spriteRenderer.color = Color.red;
    }

    public override void NewFunction()
    {
        spriteRenderer.color = Color.green;
    }
}
