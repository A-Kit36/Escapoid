using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inputs : MonoBehaviour
{
    //Lateral Movement variables
    private float verticalMovement;
    private float horizontalMovement;    

    //Abilities Input 
    private float disguise1;                   //Those are examples
    private float disguise2;
    private float interact;
    private float flashlight;
    private float pause;


    void Start()
    {
        
    }
    void Update()
    {
        //Collect player Inputs
        verticalMovement = Input.GetAxis("Vertical");
        horizontalMovement = Input.GetAxis("Horizontal");
        disguise1 = Input.GetAxis("Disguise1");      //We have to change Axis name in Input Manager
        disguise2 = Input.GetAxis("Disguise2");
        interact = Input.GetAxis("Interact");     
        flashlight = Input.GetAxis("Flashlight"); 
        pause = Input.GetAxis("Pause");           

    }
}
