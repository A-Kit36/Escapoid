using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerDialogue : MonoBehaviour
{
    [SerializeField] private DialogueAsset dialogueText;
    private bool hasRun;
    private bool isTalking;
    private void Update()
    {
        if (InputManagerOption.Instance.GetInteractInput() && isTalking)
        {
            Interact();
        }
    }
    public virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (hasRun)
            {
                return;
            }
            else
            {
                Talk(dialogueText);
                isTalking = true;
                hasRun = true;
                
            }
        }
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        isTalking = false;
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
}
