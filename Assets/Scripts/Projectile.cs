using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 1;
    public float damage = 1; 
    private void Update()
    {
        transform.Translate(Vector2.up * speed * Time.deltaTime);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (transform.tag == "PlayerProjectile")
        {
            if (collision.collider.tag == "Enemy")
            {
              //  collision.collider.GetComponent<entity>.health - damage;
            }
            
        }
        else
        {
            if (collision.collider.tag == "Player")
            {
                //  collision.collider.GetComponent<entity>.health - damage;
            }
        }
    }
}
