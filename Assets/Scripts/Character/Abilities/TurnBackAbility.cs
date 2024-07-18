using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnBackAbility : CharAbility
{
    PlayerAnimator playerAnimator;
    [SerializeField] AudioClip turnSound;
    ImposterAbility imposterAbility;
    private bool isActive = true;

    public override bool IsActive
    {
        get { return isActive; }
        set { isActive = value; }
    }

    private void Awake()
    {
        playerAnimator = GetComponent<PlayerAnimator>();
        imposterAbility = GetComponent<ImposterAbility>();
    }
    public override void Trigger()
    {
        if (!isActive)
        {
            return;
        }
        playerAnimator.ChangeBack();
        AudioPoolManager.Instance.PlayAudioClip(turnSound);
        imposterAbility.StopTimer();
    }
}
