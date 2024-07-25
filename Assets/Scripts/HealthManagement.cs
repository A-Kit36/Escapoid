using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManagement : MonoBehaviour
{
    public int health = 3;
    private Rigidbody2D enemyRb;
    public float power = 3.0f;

    void Start()
    {
        
        
    }

    void Update()
    {
        
    }

    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("EnemyWeapon"))
        {
            health--;
            enemyRb = collision.gameObject.GetComponent<Rigidbody2D>();
            GetHit(health);
            KnockBack(collision.gameObject, enemyRb);
        }
    }
    int GetHit(int health)
    {
        if (health == 3)
        {
            Debug.Log("healthy!");
        }
        if (health == 2)
        {
            Debug.Log("Hurt!");
        }
        if (health == 1)
        {
            Debug.Log("Badly hurt!");
        }
        if (health == 0)
        {
            Debug.Log("Dead! Game Over!");
        }
        return health;
    }
    void KnockBack(GameObject enemy, Rigidbody2D rb)
    {
        rb.AddForce((enemy.gameObject.transform.position - transform.position) * power, ForceMode2D.Impulse);
    }
    
    //A NE PAS OUBLIER
       //Add DRAG and MASS to enemies
       //give enemy's weapon a boxCollider and the EnemyWeapon Tag
}
