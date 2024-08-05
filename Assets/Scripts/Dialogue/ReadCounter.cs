using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReadCounter : MonoBehaviour
{
    [SerializeField] DialogueNumber dialogueNumber;
    private BoxCollider2D _collider2D;

    private void Awake()
    {
        _collider2D = GetComponent<BoxCollider2D>();
    }

    private void Start()
    {
        switch (dialogueNumber)
        {
            case DialogueNumber.FirstStory:
                if (GameManager.Instance.FirstStoryRead)
                {
                    _collider2D.enabled = false;
                }
                break;
            case DialogueNumber.SecondStoryP1:
                if (GameManager.Instance.SecondStoryP1Read)
                {
                    _collider2D.enabled = false;
                }
                break;
            case DialogueNumber.SecondStoryP2:
                if (GameManager.Instance.SecondStoryP2Read)
                {
                    _collider2D.enabled = false;
                }
                break;
            case DialogueNumber.ThirdStory:
                if (GameManager.Instance.ThirdStoryRead)
                {
                    _collider2D.enabled = false;
                }
                break;

        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.Instance.ReadLog(dialogueNumber);
        }
    }

}

public enum DialogueNumber
{
    FirstStory,
    SecondStoryP1,
    SecondStoryP2,
    ThirdStory
}
