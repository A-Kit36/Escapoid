using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StepPlatform : TriggerAbility
{
    [SerializeField] OpenFromTrigger theDoor;
    [SerializeField] private TextMeshProUGUI textbubbleUI;
    [SerializeField] private GameObject textPanel;
    [SerializeField] private AudioClip click;
    private bool deedDone = false;

    public override void OldFunction()
    {
        textbubbleUI.text = "I think I need to be heavier...";
        textPanel.SetActive(true);
    }

    public override void NewFunction()
    {
        if (deedDone)
        {
            return;
        }
        deedDone = true;
        textPanel.SetActive(false);
        theDoor.OpenDoor();
        AudioPoolManager.Instance.PlayAudioClip(click);
    }

    public override void OnTriggerExit2D(Collider2D other)
    {
        base.OnTriggerExit2D(other);
        textPanel.SetActive(false);
    }

}
