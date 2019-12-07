using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 1;
    public float damage = 1;

    private float lifeDuration = 5;
    private float lifeTime;

    private void Start()
    {
        lifeTime = lifeDuration;
    }

    private void Update()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);
        if (lifeTime > 0)
        {
            lifeTime -= Time.deltaTime;
            if (lifeTime <= 0)
                Destroy(gameObject);
        }
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
            if (collision.tag == "Building")
            {
                collision.GetComponent<Building>().ApplyDamage(damage);
            }
            if (collision.tag == "BuildingLevel")
            {
                collision.GetComponent<BuildingLevel>().mainBuilding.ApplyDamage(damage);
            }
        }
    }
}
