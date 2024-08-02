using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class OpenFromTrigger : MonoBehaviour
{
    [SerializeField] Sprite doorOpen;
    [SerializeField] private ActivateDialogue closedDialogue;
    [SerializeField] private AudioClip closedClip;
    SpriteRenderer spriteRenderer;
    private bool isOpen;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        isOpen = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!isOpen)
        {
            ShowTextBubble();
        }
        else
        {
            GameManager.Instance.NextLevel();
        }
    }

    private void ShowTextBubble()
    {
        AudioPoolManager.Instance.PlayAudioClip(closedClip);
        closedDialogue.Activate();
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        closedDialogue.Deactivate();
    }

    public void OpenDoor()
    {
        spriteRenderer.sprite = doorOpen;
        isOpen = true;
    }

}
