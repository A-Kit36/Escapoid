using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateDialogue : MonoBehaviour
{
    [SerializeField] private DialogueAsset dialogueText;
    private bool hasRun;
    private bool isTalking;
    // Update is called once per frame
    void Update()
    {
        if (InputManagerOption.Instance.GetInteractInput() && isTalking)
        {
            Interact();
        }
    }

    public void Activate()
    {
        if (hasRun)
        {
            return;
        }
        Talk(dialogueText);
        isTalking = true;
        hasRun = true;
    }

    public void Talk(DialogueAsset dialogueText)
    {
        //start convo
        DialogueBoxController.Instance.DisplayNextLine(dialogueText);
    }

    public void Interact()
    {

        DialogueBoxController.Instance.DisplayNextLine(dialogueText);
    }

    public void Deactivate()
    {
        Debug.Log("Talking deactivated");
        isTalking = false;
    }
}
