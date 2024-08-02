using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public class GetTheKey : InteractAbility
{
    [SerializeField] ActivateDialogue trueFormDialogue;
    [SerializeField] AudioClip gotkeysClip;
    private bool dialogueStarted = false;
    public bool gotTheKey = false;

    public override void OldFunction()
    {
        if (dialogueStarted)
        {
            return;
        }
        else
        {
            dialogueStarted = true;
            trueFormDialogue.Activate();
        }
    }
    public override void NewFunction()
    {
        AudioPoolManager.Instance.PlayAudioClip(gotkeysClip);
        gotTheKey = true;
        gameObject.SetActive(false);
    }

    public override void OnTriggerExit2D(Collider2D other)
    {
        base.OnTriggerExit2D(other);
        dialogueStarted = false;
        trueFormDialogue.Deactivate();
    }

}
