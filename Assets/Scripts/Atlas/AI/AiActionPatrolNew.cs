using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiActionPatrolNew : AIAction
{
    [System.Serializable]
    private struct Pivots
    {
        public Transform pivotPoint;
        public Vector2 moveDirection;
    }

    [SerializeField] Pivots[] pivotPoints; // Array of points to patrol between
    [SerializeField] float patrolSpeed; // Speed of movement
    [SerializeField] bool onStandBy;
    private int currentPointIndex = 0; // Current point in the array
    private Transform targetPoint; // Current target point
    private Rigidbody2D rb;
    private Animator animator;
    public Vector2 animDirecton;
    public override void Execute()
    {
        if (pivotPoints.Length == 0)
            return;

        // Move towards the target point
        rb.transform.position = Vector2.MoveTowards(rb.transform.position, targetPoint.position, patrolSpeed * Time.deltaTime);

        animDirecton.Set(pivotPoints[currentPointIndex].moveDirection.x, pivotPoints[currentPointIndex].moveDirection.y);
        animDirecton.Normalize();

        animator.SetFloat("Look X", animDirecton.x);
        animator.SetFloat("Look Y", animDirecton.y);

        // Check if we have reached the target point
        if (Vector2.Distance(transform.position, targetPoint.position) < 0.1f)
        {
            // Update to the next point in the array
            currentPointIndex = (currentPointIndex + 1) % pivotPoints.Length;
            targetPoint = pivotPoints[currentPointIndex].pivotPoint;
        }

        if (Mathf.Approximately(patrolSpeed, 0.0f))
        {
            animator.SetBool("IsMoving", false);
        }
    }

    public override void OnEnterState()
    {
        targetPoint = pivotPoints[currentPointIndex].pivotPoint;
        rb = GetComponentInParent<Rigidbody2D>();
        animator = GetComponentInParent<Animator>();
        animator.SetBool("IsMoving", !onStandBy);
        Debug.Log("Started patrol");
    }

    public override void OnExitState()
    {
        Debug.Log("Stopped patrol");
    }
}
