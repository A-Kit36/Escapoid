using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LockedDoor : MonoBehaviour
{
    //Message and sound that pop when interacting with door locked
    [SerializeField] private TextMeshProUGUI textbubbleUI;
    [SerializeField] private GameObject textPanel;
    [SerializeField] private AudioClip closedClip;

    //Sprites for when the door is locked then unlocked
    [SerializeField] Sprite doorOpen;
    SpriteRenderer spriteRenderer;
    
    //status of locked/unlocked
    private bool isOpen;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        isOpen = false;
    }
    //Events when interacting with the door locked OR unlocked
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!isOpen)
        {
            StartCoroutine(ShowTextBubble());
        }
        else
        {
            GameManager.Instance.NextLevel();
        }
    }
    //if door locked method
    private IEnumerator ShowTextBubble()
    {
        AudioPoolManager.Instance.PlayAudioClip(closedClip);
        textbubbleUI.text = "Closed. Maybe there's a mechanism somewhere that opens it?";
        textPanel.SetActive(true);
        yield return new WaitForSeconds(3);
        textPanel.SetActive(false);
    }
    //Method that unlock the door
    public void OpenDoor()
    {
        spriteRenderer.sprite = doorOpen;
        isOpen = true;
    }

}
