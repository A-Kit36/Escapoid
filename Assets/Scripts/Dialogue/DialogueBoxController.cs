using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.Mathematics;


public class DialogueBoxController : MonoBehaviour
{
    public static DialogueBoxController Instance { get; private set; }
    //[SerializeField] private TextMeshProUGUI NPCDialogueText;
    //[SerializeField] private GameObject dialogueBox;
    [SerializeField] private float typeSpeed = 20;
    [SerializeField] private const float MAX_TYPE_TIME = 0.1f;
    [SerializeField] private AudioClip voice;
    [SerializeField] private AudioClip typing;
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private AbilityController abilityController;


    private Queue<string> dialogue = new Queue<string>();

    public bool conversationEnded;
    public bool dialogueActive; // to stop player from moving

    //public bool dialogueFinished; // to stop the trigger from being called

    private string d;
    private bool isTyping;

    private Coroutine typeDialogueCoroutine;

    // Controlling player movement
    //public PlayerController playerController;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

    }

    public void DisplayNextLine(DialogueAsset dialogueText)
    {
        // if there is nothing in the queue
        if (dialogue.Count == 0)
        {
            if (!conversationEnded)
            {
                //start a conversation
                StartConvo(dialogueText);
            }

            else if (conversationEnded && !isTyping)
            {
                //end the conversation
                EndConvo();
                return;
            }
        }

        // if there is something in the queue

        if (!isTyping)
        {
            d = dialogue.Dequeue(); // that is going to assign the very top item in our queue to the d variable

            if (dialogueText.storyText)
            {
                typeDialogueCoroutine = StartCoroutine(TypeStoryText(d));
            }
            else
            {
                typeDialogueCoroutine = StartCoroutine(TypeDialogueText(d));
            }
        }
        else //conversation IS being typed out
        {
            if (dialogueText.storyText)
            {
                FinishStoryParagraphEarly();
            }
            else
            {
                FinishDialogueParagraphEarly();
            }

        }

        //update convo text
        //NPCDialogueText.text = d;  - at once

        //update conversationEnded bool
        if (dialogue.Count == 0)
        {
            conversationEnded = true;
        }
    }

    private void StartConvo(DialogueAsset dialogueText)
    {
        //activate gameObject (the dialogue box)
        if (!dialogueActive)
        {
            //dialogueBox.SetActive(true);
            if (dialogueText.storyText)
            {
                UiManager.Instance.StoryScreen();
            }
            else
            {
                AudioPoolManager.Instance.PlayAudioClip(voice);
                UiManager.Instance.DialogueSolo();
            }
            dialogueActive = true;
            playerMovement.DisableMovenent();
            abilityController.DisableAbilities();
            //Time.timeScale = 0;
        }

        //add text to the queue - and we are using a for loop since it's an array
        for (int i = 0; i < dialogueText.dialogue.Length; i++)
        {
            dialogue.Enqueue(dialogueText.dialogue[i]);
        }
    }

    private void EndConvo()
    {
        //clear the queue
        dialogue.Clear();
        //return bool to false
        conversationEnded = false;

        playerMovement.EnableMovenent();
        abilityController.EnableAbilities();
        //Time.timeScale = 1;
        //playerController.MoveAction.Enable(); //enabling movement

        //deactivate gameObject
        if (dialogueActive)
        {
            UiManager.Instance.MainHUD();
            //dialogueBox.SetActive(false);
            dialogueActive = false;
            //dialogueFinished = true; // this bool is only for BrokenLightReaction, we need to keep it as true otherwise it gets started up again
        }
    }

    private IEnumerator TypeDialogueText(string d)
    {
        isTyping = true;
        int maxVisibleChars = 0;
        UiManager.Instance.dialogueText.text = d;
        UiManager.Instance.dialogueText.maxVisibleCharacters = maxVisibleChars;
        //NPCDialogueText.text = d;
        //NPCDialogueText.maxVisibleCharacters = maxVisibleChars;
        Debug.Log("Coroutine Dialogue started");

        foreach (char c in d.ToCharArray())
        {
            maxVisibleChars++;
            UiManager.Instance.dialogueText.maxVisibleCharacters = maxVisibleChars;
            //NPCDialogueText.maxVisibleCharacters = maxVisibleChars;
            yield return new WaitForSeconds(MAX_TYPE_TIME / typeSpeed);
        }

        isTyping = false;
    }
    private IEnumerator TypeStoryText(string d)
    {
        isTyping = true;
        int maxVisibleChars = 0;
        UiManager.Instance.storyText.text = d;
        UiManager.Instance.storyText.maxVisibleCharacters = maxVisibleChars;
        //NPCDialogueText.text = d;
        //NPCDialogueText.maxVisibleCharacters = maxVisibleChars;
        Debug.Log("Coroutine Dialogue started");

        foreach (char c in d.ToCharArray())
        {
            maxVisibleChars++;
            UiManager.Instance.storyText.maxVisibleCharacters = maxVisibleChars;
            AudioPoolManager.Instance.PlayAudioClip(typing);
            //NPCDialogueText.maxVisibleCharacters = maxVisibleChars;
            yield return new WaitForSeconds(MAX_TYPE_TIME / typeSpeed);
        }

        isTyping = false;
    }

    private void FinishStoryParagraphEarly()
    {
        StopCoroutine(typeDialogueCoroutine);
        UiManager.Instance.storyText.maxVisibleCharacters = d.Length;
        //NPCDialogueText.maxVisibleCharacters = d.Length;
        isTyping = false;
    }

    private void FinishDialogueParagraphEarly()
    {
        StopCoroutine(typeDialogueCoroutine);
        UiManager.Instance.dialogueText.maxVisibleCharacters = d.Length;
        //NPCDialogueText.maxVisibleCharacters = d.Length;
        isTyping = false;
    }
}
