using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : entity
{
    public float movementSpeed = 6;

    protected const float GRAVITY = 2000f;

    protected float jumpSpeed = 9;
    protected float currentJumpSpeed;
    protected float jumpDecreaseSpeed = 18;

    protected bool jumping;

    public float attackCooldownDuration = .5f;
    protected float attackCooldownTime;

    public virtual void Update()
    {
        if (jumping)
        {
            transform.position += Vector3.up * Time.deltaTime * currentJumpSpeed;// GRAVITY) ;
            currentJumpSpeed += Time.deltaTime * -1 * jumpDecreaseSpeed;
        }
        if (attackCooldownTime > 0)
            attackCooldownTime -= Time.deltaTime;
    }

    public override void OnDeath()
    {

    }
}
