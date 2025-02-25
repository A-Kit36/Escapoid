using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    private float verticalMovement;
    private float horizontalMovement;  
    public float speed = 5.0f;

    private Rigidbody2D _rigidbody2D;

    void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        verticalMovement = Input.GetAxis("Vertical");
        horizontalMovement = Input.GetAxis("Horizontal");

        /*transform.Translate(Vector2.up * speed * Time.deltaTime * verticalMovement);
        transform.Translate(Vector2.right * speed * Time.deltaTime * horizontalMovement);*/

        _rigidbody2D.velocity = new Vector2(horizontalMovement * speed, verticalMovement * speed);
    }
}
