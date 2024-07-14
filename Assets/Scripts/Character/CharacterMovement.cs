using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    [SerializeField] float speed;
    private Vector2 movementInput;
    Rigidbody2D rb;
    private Vector3 targetPos; // for tile-based movement
    public LayerMask solidObjectsLayer; // to check if our target tile has a solid collider, solid objects will be on a Layer called "SolidObjects"

    private bool isMovingToTile; // explicitly for the coroutine
    private bool isMoving; // for animator

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
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

        if (IsWalkable(targetPos))
        {
            StartCoroutine(MoveToPosition(targetPos));
        }

    }

    IEnumerator MoveToPosition(Vector3 target)
    {
        isMovingToTile = true;

        while ((target - transform.position).sqrMagnitude > Mathf.Epsilon)
        {
            rb.transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
            yield return null;
        }
        rb.transform.position = target;

        isMovingToTile = false;
    }

    private bool IsWalkable(Vector3 targetPos)
    {
        if (Physics2D.OverlapCircle(targetPos, 1f, solidObjectsLayer) != null)
        {
            return false;
        }
        else
        {
            return true;
        }
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
