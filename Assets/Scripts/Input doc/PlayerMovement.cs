using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    [SerializeField] float speed;
    private Vector2 movementInput;
    Rigidbody2D rb;
    private Vector3 targetPos;
    //public LayerMask solidObjectsLayer; // to check if our target tile has a solid collider, solid objects will be on a Layer called "SolidObjects"

    private bool isMovingToTile; // explicitly for the coroutine
    private bool isMoving; // for animator

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        HandleMovement();
    }

    void HandleMovement()
    {
        if (isMovingToTile)
        {
            return;
        }

        movementInput.x = InputManagerOption.Instance.GetHorizontalInput();
        movementInput.y = InputManagerOption.Instance.GetVerticalInput();

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

        rb.transform.position = Vector3.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);

        /* if (IsWalkable(targetPos))
        {
            StartCoroutine(MoveToPosition(targetPos));
        } */

    }

    /* IEnumerator MoveToPosition(Vector3 target)
    {
        isMovingToTile = true;

        while ((target - transform.position).sqrMagnitude > Mathf.Epsilon)
        {
            rb.transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
            yield return null;
        }
        rb.transform.position = target;

        isMovingToTile = false;
    } */

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

    /* private bool IsWalkable(Vector3 targetPos)
    {
        if (Physics2D.OverlapCircle(targetPos, 0.5f, solidObjectsLayer) != null)
        {
            return false;
        }
        else { return true; }
    } */
}
