using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LightAbility : CharAbility
{
    Light2D light2D;

    private void Awake()
    {
        light2D = GetComponent<Light2D>();
        light2D.enabled = false;
    }
    public override void Trigger()
    {
        light2D.enabled = true;
    }

    public void Disable()
    {
        light2D.enabled = false;
    }
}
