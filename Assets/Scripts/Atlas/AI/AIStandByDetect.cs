using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIStandByDetect : AIDecision
{
    [SerializeField] private float detectionRange; // Range of the line of sight
    [SerializeField] LineRenderer lineRenderer;
    private Vector2 standByDir = Vector2.down;
    private Rigidbody2D rb;

    public override bool Evaluate()
    {
        return DetectTarget();
    }

    private bool DetectTarget()
    {
        RaycastHit2D hit = Physics2D.Raycast(rb.transform.position, standByDir, detectionRange, LayerMask.GetMask("Player"));
        //Debug.DrawRay(rb.transform.position, standByDir * detectionRange, Color.red); // Draw ray for visual debugging

        Vector3 lineVector = new Vector3(standByDir.x, standByDir.y, 0);

        lineRenderer.positionCount = 2;
        lineRenderer.SetPosition(0, rb.transform.position);
        lineRenderer.SetPosition(1, rb.transform.position + lineVector * detectionRange);

        if (hit.collider != null /* && hit.collider.CompareTag("Player") */)
        {
            // Player detected, execute additional logic
            IRoleAssignable roleAssignable = hit.collider.GetComponent<IRoleAssignable>();
            Role playerRole = roleAssignable.UserRole;
            if (playerRole != Role.PrisonGuard)
            {
                return true;
            }
            return false;
        }
        else
        {
            return false;
        }
    }

    public override void OnEnterState()
    {
        rb = GetComponentInParent<Rigidbody2D>();
        lineRenderer.enabled = true;
    }

    public override void OnExitState()
    {
        lineRenderer.enabled = false;
    }
}
