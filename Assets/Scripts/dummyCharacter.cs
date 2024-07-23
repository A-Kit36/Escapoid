using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dummyCharacter : MonoBehaviour, IRoleAssignable
{

    public AIBrain Brain;

    [SerializeField] private Role userRole;

    public Role UserRole
    {
        get { return userRole; }
        set { userRole = value; }
    }
    private Animator _animator;
    private Rigidbody2D _rigidbody2D;

    private void Awake()
    {
        _animator = GetComponent<Animator>(); //real version gets this off the model. no model set then look to this gameobject.
        _rigidbody2D = GetComponent<Rigidbody2D>();
        //Brain.SetCharacter(this); //set brain in-editor. only do this for AI Characters. Can look through this gameobject and children gameobjects to look for a brain first.
    }

    public void MoveTowardsTarget(Transform target, float speed)
    {
        float step = speed * Time.deltaTime;
        transform.position = Vector2.MoveTowards(transform.position, target.position, step);
    }

    public void UpdateAnimatorBool(string name, bool value)
    {
        _animator.SetBool(name, value);
    }

    public void UpdateAnimatorFloat(string name, float value)
    {
        _animator.SetFloat(name, value);
    }
}
