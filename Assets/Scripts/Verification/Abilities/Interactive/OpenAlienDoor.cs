using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public class OpenAlienDoor : InteractAbility
{
    [SerializeField] ActivateDialogue nohandsDialogue;
    [SerializeField] GameObject closedDoor;
    [SerializeField] Sprite openDoorsprite;
    [SerializeField] GameObject movingAlien;
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private TextMeshProUGUI NPCDialogueText;
    [SerializeField] private GameObject dialogueBox;
    [SerializeField] AudioClip openDoorClip;
    private SpriteRenderer doorRenderer;
    private bool dialogueStarted = false;
    private bool coroutineStarted = false;

    private void Awake()
    {
        doorRenderer = closedDoor.GetComponent<SpriteRenderer>();
    }
    public override void OldFunction()
    {
        if (dialogueStarted)
        {
            return;
        }
        else
        {
            dialogueStarted = true;
            nohandsDialogue.Activate();
        }
    }
    public override void NewFunction()
    {
        if (coroutineStarted)
        {
            return;
        }
        StartCoroutine(BigMistake());
    }

    public override void OnTriggerExit2D(Collider2D other)
    {
        base.OnTriggerExit2D(other);
        dialogueStarted = false;
        nohandsDialogue.Deactivate();
    }

    private IEnumerator BigMistake()
    {
        coroutineStarted = true;
        playerMovement.DisableMovenent();
        InputManagerOption.Instance.DisableAllButtons();
        AudioPoolManager.Instance.PlayAudioClip(openDoorClip);
        doorRenderer.sprite = openDoorsprite;
        NPCDialogueText.text = "Thank you.";
        dialogueBox.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        dialogueBox.SetActive(false);
        Vector2 startPosition = movingAlien.transform.position;
        Vector2 endPosition = startPosition + Vector2.down * 2;
        while (Vector2.Distance(movingAlien.transform.position, endPosition) > 0.05f) // Small threshold to ensure reaching the end
        {
            movingAlien.transform.position = Vector2.MoveTowards(movingAlien.transform.position, endPosition, 1.5f * Time.deltaTime);
            yield return null; // Wait until the next frame
        }
        movingAlien.transform.position = endPosition;
        InputManagerOption.Instance.EnableAllButtons();
        GameManager.Instance.HandleGameOver();
    }
}
