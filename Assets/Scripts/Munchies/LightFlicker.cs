using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LightFlicker : MonoBehaviour
{
    private Light2D light2D;
    float interval = 1.0f;
    float timer;
    [SerializeField] float maxWait = 1;
    [SerializeField] float maxFlicker = 0.2f;

    private void Awake()
    {
        light2D = GetComponent<Light2D>();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer > interval)
        {
            ToggleLight();
        }
    }

    private void ToggleLight()
    {
        light2D.enabled = !light2D.enabled;

        if (light2D.enabled)
        {
            interval = Random.Range(0, maxWait);
        }
        else
        {
            interval = Random.Range(0, maxFlicker);
        }

        timer = 0f;
    }
}
