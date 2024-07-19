using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;

public class AIDecisionLineDetection : AIDecision
{
    public float DetectionDistance = 10f;
    public float RayWidth = 1f;
    public LayerMask TargetLayer;

    [SerializeField]
    private LineRenderer _lineRenderer;
    

    private void OnValidate()
    {
        if(_lineRenderer == null)
        {            
            _lineRenderer = GetComponent<LineRenderer>();
        }
    }

    public override bool Evaluate()
    {
        return DetectTarget();
    }

    private bool DetectTarget()
    {
        Transform target = null;
        RaycastHit2D raycast;
        Vector2 direction = Brain.Character.CardinalToVect3(Brain.Character.FacingDirection);
        raycast = Physics2D.BoxCast(transform.position - Vector3.right * RayWidth/ 2f, Vector2.one * RayWidth, 0f, direction, DetectionDistance, TargetLayer);

        DrawDetectionLine(direction);                

        if (raycast)
        {
            Brain.Target = raycast.collider.gameObject.transform;
            return true;
        }
        else
        {
            return false;
        }
    }

    

    private void DrawDetectionLine(Vector3 dir)
    {
        //Debug.DrawLine(transform.position, transform.position + Vector3.right * 10);

        //TODO draw line based on direction Character is facing
        if (_lineRenderer != null)
        {
            _lineRenderer.SetPosition(0, transform.position);
            _lineRenderer.SetPosition(1, transform.position + dir * DetectionDistance);
            _lineRenderer.startColor = Color.red;
            _lineRenderer.endColor = Color.red;
            _lineRenderer.startWidth = RayWidth;
            _lineRenderer.endWidth = RayWidth;
        }        
    }

    public override void OnEnterState()
    {
        if (_lineRenderer != null)
        {
            _lineRenderer.enabled = true;
        }
        
    }

    public override void OnExitState()
    {
        if (_lineRenderer != null)
        {
            _lineRenderer.enabled = false;
        }
    }
}
