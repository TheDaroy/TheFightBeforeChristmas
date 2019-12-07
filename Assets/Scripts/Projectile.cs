using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 1;
    public float damage = 1; 
    private void Update()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (transform.tag == "PlayerProjectile")
        {
            if (collision.tag == "Enemy")
            {
                collision.GetComponent<entity>().ApplyDamage(damage);
                Destroy(gameObject);
            }

        }
        else
        {
            if (collision.tag == "Player")
            {
                collision.GetComponent<entity>().ApplyDamage(damage);
                Destroy(gameObject);
            }
        }
    }
}
