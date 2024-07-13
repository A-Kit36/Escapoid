using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    Animator animator;
    [SerializeField] RuntimeAnimatorController ogAnimatorController; // player's OG form
    Vector2 moveDirection = new Vector2(); //setting it like this so the player looks down from the beginning
    CharacterMovement characterMovement;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        characterMovement = GetComponent<CharacterMovement>();
    }

    // Update is called once per frame
    void Update()
    {

        moveDirection.x = characterMovement.GetPlayerX();
        moveDirection.y = characterMovement.GetPlayerY();
        moveDirection.Set(moveDirection.x, moveDirection.y);
        moveDirection.Normalize();

        animator.SetFloat("Look X", moveDirection.x);
        animator.SetFloat("Look Y", moveDirection.y);
        animator.SetBool("IsMoving", characterMovement.IsMoving());
    }

    public void ChangeSkin()
    {
        animator.SetTrigger("Transform");
    }

    public void ChangeController(RuntimeAnimatorController runtimeAnimatorController)
    {
        animator.runtimeAnimatorController = runtimeAnimatorController;
    }

    public void ChangeBack()
    {
        animator.runtimeAnimatorController = ogAnimatorController;
    }

}
