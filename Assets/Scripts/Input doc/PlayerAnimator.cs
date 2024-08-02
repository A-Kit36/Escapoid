using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    Animator animator;
    [SerializeField] RuntimeAnimatorController ogAnimatorController; // player's OG form
    [SerializeField] AudioClip turnSound; // made sense to put this sound here because other transform abilities use the same sound
    Vector2 moveDirection = new Vector2(); //setting it like this so the player looks down from the beginning
    PlayerMovement characterMovement;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        characterMovement = GetComponent<PlayerMovement>();
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

    public void ChangeSkin(RuntimeAnimatorController receivedController)
    {
        StartCoroutine(TurnIntoAlien(receivedController));
    }

    public void ChangeController(RuntimeAnimatorController runtimeAnimatorController)
    {
        animator.runtimeAnimatorController = runtimeAnimatorController;
    }

    public void ChangeBack()
    {
        StartCoroutine(ToTrueForm());
    }

    public void GoIntoShell()
    {
        animator.SetTrigger("Shell"); // for shelllurking only
    }

    public void UnShell()
    {
        animator.SetTrigger("UnShell"); // for shelllurking only
    }

    private IEnumerator TurnIntoAlien(RuntimeAnimatorController newController)
    {
        AudioPoolManager.Instance.PlayAudioClip(turnSound);
        animator.SetTrigger("Transform");
        yield return new WaitForSeconds(0.7f);
        ChangeController(newController);
    }

    private IEnumerator ToTrueForm()
    {
        AudioPoolManager.Instance.PlayAudioClip(turnSound);
        animator.SetTrigger("Transform");
        yield return new WaitForSeconds(0.7f);
        animator.runtimeAnimatorController = ogAnimatorController;
    }

}
