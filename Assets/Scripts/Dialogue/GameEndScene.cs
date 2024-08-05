using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEndScene : MonoBehaviour
{
    [SerializeField] private DialogueAsset dialogueText;
    [SerializeField] GameObject homePlanet;
    [SerializeField] GameObject titleCard;
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private AbilityController abilityController;
    private bool hasRun;
    private bool isTalking;
    private bool coroutineStarted;
    private void Update()
    {
        if (InputManagerOption.Instance.GetInteractInput() && isTalking)
        {
            if (!DialogueBoxController.Instance.dialogueActive)
            {
                return;
            }
            Interact();
        }

        if (isTalking && !DialogueBoxController.Instance.dialogueActive && !coroutineStarted)
        {
            StartCoroutine(GameEnd());
            return;
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

    private IEnumerator GameEnd()
    {
        coroutineStarted = true;
        playerMovement.DisableMovenent();
        abilityController.DisableAbilities();
        homePlanet.SetActive(true);
        yield return new WaitForSeconds(3);
        titleCard.SetActive(true);
        yield return new WaitForSeconds(3);
        SoundManager.Instance.StopMusic();
        GameManager.Instance.HandleGameEnd();
    }
}
