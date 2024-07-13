using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    [SerializeField] float speed;
    private Vector2 movementInput;
    private Vector3 targetPos; // for tile-based movement

    private bool isMovingToTile; // explicitly for the coroutine
    private bool isMoving; // for animator

    // Update is called once per frame
    void Update()
    {
        HandleMovement();
    }

    void HandleMovement()
    {
        if (isMovingToTile)
        {
            return;
        }

        movementInput.x = InputManager.Instance.GetHorizontalInput();
        movementInput.y = InputManager.Instance.GetVerticalInput();

        // removing diagonal movement
        if (movementInput.x != 0)
        {
            movementInput.y = 0;
        }

        if (!Mathf.Approximately(movementInput.x, 0.0f) || !Mathf.Approximately(movementInput.y, 0.0f))
        {
            isMoving = true;
        }
        else
        {
            isMoving = false;
        }


        targetPos = transform.position;
        targetPos.x += movementInput.x;
        targetPos.y += movementInput.y;
        StartCoroutine(MoveToPosition(targetPos));

    }

    IEnumerator MoveToPosition(Vector3 target)
    {
        isMovingToTile = true;

        while ((target - transform.position).sqrMagnitude > Mathf.Epsilon)
        {
            transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
            yield return null;
        }
        transform.position = target;

        isMovingToTile = false;
    }

    public float GetPlayerX()
    {
        return movementInput.x;
    }

    public float GetPlayerY()
    {
        return movementInput.y;
    }

    public bool IsMoving()
    {
        return isMoving;
    }
}
