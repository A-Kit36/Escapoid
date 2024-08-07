using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerFade : MonoBehaviour
{
    [SerializeField] float endValue;
    [SerializeField] float duration;

    private void OnTriggerEnter2D(Collider2D other)
    {
        StartCoroutine(SoundManager.Instance.LerpFunction(endValue, duration));
    }
}
