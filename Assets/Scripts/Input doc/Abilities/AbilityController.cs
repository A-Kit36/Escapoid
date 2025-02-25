using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityController : MonoBehaviour
{
    [SerializeField] ImposterAbility imposterAbility;
    [SerializeField] TurnBackAbility turnBackAbility;
    [SerializeField] LightAbility lightAbility;

    RoleController roleController;

    private void Awake()
    {
        roleController = GetComponent<RoleController>();
    }

    private void Update()
    {
        if (InputManagerOption.Instance.GetImposterInput())
        {
            imposterAbility.Trigger();
            if (roleController.UserRole == Role.VisionAlien)
            {
                lightAbility.Trigger();
            }
        }

        if (InputManagerOption.Instance.GetTurnBackInput())
        {
            turnBackAbility.Trigger();
        }

        if (!imposterAbility.IsImposter)
        {
            lightAbility.Disable();
        }
    }

    // here we can also deactivate abilities when needed
    // I wish there was a way to control the ability animations from here, so we don't have to fetch animator for every ability individually, but I am still not sure how to do it best
}
