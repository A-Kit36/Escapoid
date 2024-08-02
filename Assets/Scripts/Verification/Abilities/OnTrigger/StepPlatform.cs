using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StepPlatform : TriggerAbility
{
    [SerializeField] OpenFromTrigger theDoor;
    [SerializeField] private AudioClip click;
    [SerializeField] private ActivateDialogue toosmallDialogue;
    [SerializeField] private ActivateDialogue clickedPanel;
    private bool deedDone = false;
    private bool oldFuncActivated = false;

    public override void OldFunction()
    {
        if (oldFuncActivated)
        {
            return;
        }
        toosmallDialogue.Activate();
        //textbubbleUI.text = "I think I need to be heavier...";
        //textPanel.SetActive(true);
    }

    public override void NewFunction()
    {
        if (deedDone)
        {
            return;
        }
        toosmallDialogue.Deactivate();
        deedDone = true;
        theDoor.OpenDoor();
        clickedPanel.Activate();
        AudioPoolManager.Instance.PlayAudioClip(click);
    }


    public override void OnTriggerExit2D(Collider2D other)
    {
        base.OnTriggerExit2D(other);
        toosmallDialogue.Deactivate();
        clickedPanel.Deactivate();
    }


}
