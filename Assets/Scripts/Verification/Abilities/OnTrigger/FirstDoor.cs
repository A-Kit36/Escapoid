using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstDoor : DoorOpen
{
    BoxCollider2D boxCollider2D;

    private void Awake()
    {
        BoxCollider2D[] colliders = GetComponentsInChildren<BoxCollider2D>(true);

        foreach (BoxCollider2D col in colliders)
        {
            // Check if the collider's GameObject is not the parent
            if (col.gameObject != this.gameObject)
            {
                boxCollider2D = col;
            }
        }
    }

    public override void NewFunction()
    {
        AudioPoolManager.Instance.PlayAudioClip(acceptedSound);
        spriteRenderer.sprite = openSprite;
        boxCollider2D.enabled = false;

    }
}
