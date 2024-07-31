using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressForText : NPC
{
    [SerializeField] private DialogueAsset dialogueText;

    public override void Interact()
    {
        Talk(dialogueText);
    }

    public void Talk(DialogueAsset dialogueText)
    {
        //start convo
        DialogueBoxController.Instance.DisplayNextLine(dialogueText);
    }
}
