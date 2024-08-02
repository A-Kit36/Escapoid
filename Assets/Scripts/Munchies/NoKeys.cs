using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoKeys : TriggerDialogue
{
    [SerializeField] GetTheKey getTheKey;

    public override void OnTriggerEnter2D(Collider2D other)
    {

        if (getTheKey.gotTheKey)
        {
            gameObject.SetActive(false);
        }
        else
        {
            base.OnTriggerEnter2D(other);
        }
    }

}
