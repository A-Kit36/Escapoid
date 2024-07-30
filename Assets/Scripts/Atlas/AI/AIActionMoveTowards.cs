using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIActionMoveTowards : AIAction
{
    [SerializeField] float chaseSpeed;
    [SerializeField] float catchDistance;
    private float distance;
    private Rigidbody2D rb;
    private Animator animator;
    //private CharacterMovement _charMovement;

    public override void Execute()
    {
        Transform targetTransform = Brain.Target.transform;

        //Transform charTransform = Brain.Character.transform;

        if (targetTransform.position.x > rb.transform.position.x)
        {
            //_charMovement.MoveCharacter(Cardinal.East);
            rb.transform.position = Vector3.MoveTowards(rb.transform.position, targetTransform.position, chaseSpeed * Time.deltaTime);
            animator.SetFloat("Look X", 1);
            animator.SetFloat("Look Y", 0);
        }
        else if (targetTransform.position.x < rb.transform.position.x)
        {
            rb.transform.position = Vector3.MoveTowards(rb.transform.position, targetTransform.position, chaseSpeed * Time.deltaTime);
            animator.SetFloat("Look X", -1);
            animator.SetFloat("Look Y", 0);
            //_charMovement.MoveCharacter(Cardinal.West);
        }
        else if (targetTransform.position.y > rb.transform.position.y)
        {
            rb.transform.position = Vector3.MoveTowards(rb.transform.position, targetTransform.position, chaseSpeed * Time.deltaTime);
            animator.SetFloat("Look X", 0);
            animator.SetFloat("Look Y", 1);
            //_charMovement.MoveCharacter(Cardinal.North);
        }
        else
        {
            rb.transform.position = Vector3.MoveTowards(rb.transform.position, targetTransform.position, chaseSpeed * Time.deltaTime);
            animator.SetFloat("Look X", 0);
            animator.SetFloat("Look Y", -1);
            //_charMovement.MoveCharacter(Cardinal.South);
        }

        distance = Vector2.Distance(targetTransform.position, rb.transform.position);
        if (distance <= catchDistance)
        {
            Debug.Log("Game Over!");
            GameManager.Instance.HandleGameOver();
        }
    }

    public override void OnEnterState()
    {
        /* _charMovement = (CharacterMovement)Brain.Character._abilities.Find(p => p.AbilityName == "Movement");
        if (_charMovement == null) 
        {
            Debug.Log("AI Agent needs CharacterMovement to utilize AIActionMoveTowards.");
        } */
        rb = GetComponentInParent<Rigidbody2D>();
        animator = GetComponentInParent<Animator>();
        animator.SetBool("IsMoving", true);
    }

    public override void OnExitState()
    {
    }
}
