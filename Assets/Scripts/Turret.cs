using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : entity
{
    
    List<entity> enemiesInRange = new List<entity>();
    entity currentTarget;
    public GameObject projectile;
    public TurretPlatform platform;
    

    public float fireRate;
    float fireTimer;
 
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            Debug.Log("Hit");

            enemiesInRange.Add(collision.transform.GetComponent<entity>());
            if (currentTarget == null)
            {
                currentTarget = collision.transform.GetComponent<entity>();
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
        Debug.Log("Exit");

            entity temp = collision.GetComponent<entity>();
            for (int i = 0; i < enemiesInRange.Count; i++)
            {
                if (enemiesInRange[i].index == temp.index)
                {
                    
                    if (currentTarget == enemiesInRange[i])
                    {
                        currentTarget = null;
                    }
                    enemiesInRange.Remove(enemiesInRange[i]);
                }
            }
        }
    }
    private void Update()
    {
        if (currentTarget && currentTarget.Dead)
        {
            enemiesInRange.Remove(currentTarget);
        }
        LookAtClosestTarget();
        if (currentTarget)
        {
            Fire();
        }
        Debug.Log(currentTarget);

        if (health <= 0)
        {
            platform.activeTurret = false;
        }
    }

    void Fire()
    {
        fireTimer = fireTimer + Time.deltaTime;
        if (fireTimer >= fireRate)
        {
            FindClosestTarget();
            LookAtClosestTarget();
            GameObject temp = Instantiate(projectile, transform.position, transform.rotation);
            fireTimer = 0;
        }
    }
    void LookAtClosestTarget()
    {
        if (!currentTarget)
            return;
        var dir = currentTarget.transform.position - transform.position;
        var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }
    void FindClosestTarget()
    {
        if (currentTarget != null)
        {
            for (int i = 0; i < enemiesInRange.Count; i++)
            {

                float dist = Vector2.Distance(transform.position, enemiesInRange[i].transform.position);
                if (dist < Vector2.Distance(transform.position, currentTarget.transform.position))
                {
                    currentTarget = enemiesInRange[i].GetComponent<entity>();
                }
            }
        }   
    }
}
