using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpen : TriggerAbility
{
    SpriteRenderer spriteRenderer;
    [SerializeField] Sprite openSprite;
    [SerializeField] AudioClip deniedSound;
    [SerializeField] AudioClip acceptedSound;

    // Start is called before the first frame update
    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public override void OldFunction()
    {
        AudioPoolManager.Instance.PlayAudioClip(deniedSound);
        //spriteRenderer.color = Color.red;
    }

    public override void NewFunction()
    {
        AudioPoolManager.Instance.PlayAudioClip(acceptedSound);
        spriteRenderer.sprite = openSprite;
        //gameObject.SetActive(false);
        //spriteRenderer.color = Color.green;
    }

    public override void OnTriggerStay2D(Collider2D other)
    {

    }

}
