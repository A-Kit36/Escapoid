using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
//using UnityEngine.InputSystem;

public class PlayerInputControl : MonoBehaviour
{
    private Rigidbody2D rb;
    // instead of using PlayerInput component, I generated a C# script from the Input Actions for better control
    //private PlayerInputActions playerInputActions;
    private Vector2 movementInput;
    private Vector3 targetPos; // for tile-based movement
    //private InputAction imposterAction; //button E
    //private InputAction turnbackAction; //button Q
    [SerializeField] float speed = 5f;
    [SerializeField] float attackPosition = 1;
    [SerializeField] AudioClip pewpew;
    [SerializeField] AudioClip crunch;
    private bool isMoving; //for animation and other things

    // Animation
    Animator animator;
    Vector2 moveDirection = new Vector2(0, -1); //setting it like this so the player looks down from the beginning

    // getting a controller so we can change it at runtime
    RuntimeAnimatorController enemyController;
    RuntimeAnimatorController ogController; //so we can go back to the original alien form

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        //playerInputActions = new PlayerInputActions();
        //playerInputActions.Player.Enable();

        //imposterAction = playerInputActions.Player.Imposter;
        //  turnbackAction = playerInputActions.Player.TurnBack;

        animator = GetComponent<Animator>();
        ogController = animator.runtimeAnimatorController;
    }

    private void Update()
    {
        //movementInput = playerInputActions.Player.Movement.ReadValue<Vector2>();

        // removing diagonal movement
        if (movementInput.x != 0)
        {
            movementInput.y = 0;
        }
        targetPos = transform.position;
        targetPos.x += movementInput.x;
        targetPos.y += movementInput.y;

        if (!isMoving)
        {
            StartCoroutine(MoveToPosition(targetPos));
        }

        if (false)//turnbackAction.triggered)
        {
            //AudioPoolManager.Instance.PlayAudioClip(pewpew);
            //TurnBack();
        }
    }

    private void FixedUpdate()
    {
        //Vector2 inputVector = playerInputActions.Player.Movement.ReadValue<Vector2>();

        /*if (inputVector.x != 0)
        {
            inputVector.y = 0;
        }*/

        // needed for animator and other potential things
        /*if (!Mathf.Approximately(inputVector.x, 0.0f) || !Mathf.Approximately(inputVector.y, 0.0f))
        {
            moveDirection.Set(inputVector.x, inputVector.y);
            moveDirection.Normalize();
        }*/

        animator.SetFloat("Look X", moveDirection.x);
        animator.SetFloat("Look Y", moveDirection.y);
        animator.SetBool("isMoving", isMoving);
    }

    private IEnumerator MoveToPosition(Vector3 target)
    {
        isMoving = true;

        while ((target - transform.position).sqrMagnitude > Mathf.Epsilon)
        {
            transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
            yield return null;
        }
        transform.position = target;
        isMoving = false;

    }

    // checking to see if we are inside the trigger zone so we can change skins
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            Animator enemyAnimator = other.GetComponent<Animator>();
            enemyController = enemyAnimator.runtimeAnimatorController; //getting the controller the enemy animator uses
            GameObject enemy = other.gameObject;
            Vector2 enemyPosition = other.gameObject.transform.position;
            Vector2 behindyouPosition = new Vector2(enemyPosition.x, enemyPosition.y + attackPosition);

            if (false)//imposterAction.triggered)
            {
                //AudioPoolManager.Instance.PlayAudioClip(crunch);
                //StartCoroutine(ImposterEnemy(enemyController, behindyouPosition, enemy, enemyAnimator)); //pass the new controller to the coroutine
            }

        }
    }

    private IEnumerator ImposterEnemy(RuntimeAnimatorController runtimeAnimatorController, Vector2 vector2, GameObject gameObject, Animator enemyanimator)
    {
        //playerInputActions.Player.Disable();
        rb.transform.position = vector2;
        moveDirection.Set(0, -1);
        animator.SetTrigger("Devour");
        enemyanimator.SetTrigger("Eaten");
        yield return new WaitForSeconds(0.7f);
        Destroy(gameObject);
        animator.runtimeAnimatorController = enemyController;
        //playerInputActions.Player.Enable();
        yield return null;

    }

    private void TurnBack()
    {
        animator.runtimeAnimatorController = ogController;
    }

}
