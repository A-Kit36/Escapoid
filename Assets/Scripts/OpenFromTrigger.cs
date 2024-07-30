using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class OpenFromTrigger : MonoBehaviour
{
    [SerializeField] Sprite doorOpen;
    [SerializeField] private TextMeshProUGUI textbubbleUI;
    [SerializeField] private GameObject textPanel;
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
            StartCoroutine(ShowTextBubble());
        }
        else
        {
            GameManager.Instance.NextLevel();
        }
    }

    private IEnumerator ShowTextBubble()
    {
        AudioPoolManager.Instance.PlayAudioClip(closedClip);
        textbubbleUI.text = "Closed. Maybe there's a mechanism somewhere that opens it?";
        textPanel.SetActive(true);
        yield return new WaitForSeconds(3);
        textPanel.SetActive(false);
    }

    public void OpenDoor()
    {
        spriteRenderer.sprite = doorOpen;
        isOpen = true;
    }

}
