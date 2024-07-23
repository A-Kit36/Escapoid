using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ChangeText : InteractAbility
{
    [SerializeField] private TextMeshProUGUI note; // temporary to check 
    [SerializeField] GameObject textBox;
    public override void OldFunction()
    {
        note.text = "you are an alien";
        textBox.SetActive(true);
    }

    public override void NewFunction()
    {
        note.text = "you are a guard";
        textBox.SetActive(true);
    }

    public override void OnTriggerExit2D(Collider2D other)
    {
        base.OnTriggerExit2D(other);
        textBox.SetActive(false);
    }
}
