using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class NPC : MonoBehaviour
{
    private Transform _playerTransform;

    private void Start()
    {
        _playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        if (InputManagerOption.Instance.GetInteractInput() && IsWithinInteractDistance())
        {
            Interact();
        }
    }

    public abstract void Interact();

    private bool IsWithinInteractDistance()
    {
        if (Vector2.Distance(_playerTransform.position, transform.position) < 1.7f)
        {
            return true;
        }

        else
        {
            return false;
        }
    }

}



