using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class entity : MonoBehaviour
{
    protected float movementSpeed = 6;

    public float maxHealth = 100;
    protected float health;

    protected const float GRAVITY = 2000f;

    protected float jumpSpeed = 9;
    protected float currentJumpSpeed;
    protected float jumpDecreaseSpeed = 18;

    protected bool jumping;
    
    public bool Dead {
        get { return health <= 0; }
    }

    public virtual void Update()
    {
        if (jumping)
        {
            transform.position += Vector3.up * Time.deltaTime * currentJumpSpeed;// GRAVITY) ;
            currentJumpSpeed += Time.deltaTime * -1 * jumpDecreaseSpeed;
        }
    }
    public void ApplyDamage(float damage)
    {
        health -= damage;
    }

}
