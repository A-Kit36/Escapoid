using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiLineDetectNew : AIDecision
{
    [SerializeField] private float detectionRange; // Range of the line of sight
    [SerializeField] LineRenderer lineRenderer;
    private AiActionPatrolNew aiPatrol;
    private Rigidbody2D rb;
    private Vector2 gizmosDir;

    public override bool Evaluate()
    {
        return DetectTarget();
    }

    private bool DetectTarget()
    {
        RaycastHit2D hit = Physics2D.Raycast(rb.transform.position, aiPatrol.animDirecton, detectionRange, LayerMask.GetMask("Player"));
        //Debug.DrawRay(rb.transform.position, aiPatrol.animDirecton * detectionRange, Color.red); // Draw ray for visual debugging

        Vector3 lineVector = new Vector3(aiPatrol.animDirecton.x, aiPatrol.animDirecton.y, 0);

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
        aiPatrol = GetComponent<AiActionPatrolNew>();
        rb = GetComponentInParent<Rigidbody2D>();
        lineRenderer.enabled = true;
    }

    public override void OnExitState()
    {
        lineRenderer.enabled = false;
    }

    /*  public void OnDrawGizmos()
     {
         // Draw line of sight for debugging purposes
         aiPatrol = GetComponent<AiActionPatrolNew>();
         Gizmos.color = Color.red;
         gizmosDir = aiPatrol.animDirecton;
         Gizmos.DrawLine(transform.position, (Vector2)transform.position + gizmosDir * detectionRange);
     } */
}
