using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyState { Walk, Attack }

public class Enemy : Character
{
    protected EnemyState state;
    public bool flying;

    public Transform towerTarget;
    private Transform currentTarget;

    public override void Start()
    {
        base.Start();
        state = EnemyState.Walk;
        jumping = true;
        currentTarget = towerTarget;
    }

    public override void Update()
    {
        //base.Update();

        if (attackCooldownTime > 0)
            attackCooldownTime -= Time.deltaTime;
        HandleStates();
    }

    public void SetTarget(Transform target)
    {
        currentTarget = target;
    }

    void HandleStates()
    {
        switch (state)
        {
            case EnemyState.Walk:
                Walk();
                break;
            case EnemyState.Attack:
                Attack();
                break;
            default:
                break;
        }
    }

    void Walk()
    {
        if (currentTarget == null) {
            Debug.Log(gameObject.name + " got no target, man");
            return;
        }
        Vector3 move = transform.position;
        if (transform.position.x < currentTarget.position.x) {
            move += (Vector3.right * movementSpeed * Time.deltaTime);

            if (move.x > currentTarget.position.x)
                move = new Vector2(currentTarget.position.x, move.y);
            
        }
        else if (transform.position.x > currentTarget.position.x) {
            move += (Vector3.left * movementSpeed * Time.deltaTime);
            
            if (move.x < currentTarget.position.x)
                move = new Vector2(currentTarget.position.x, move.y);
            
        }
        transform.position = move;
        if (CheckProximity(currentTarget))
            state = EnemyState.Attack;
    }

    void Attack()
    {
        if (attackCooldownTime <= 0)
        {
            entity currentEntity = currentTarget.GetComponent<entity>();
            if (currentEntity)
            {
                currentEntity.ApplyDamage(20);
                attackCooldownTime = attackCooldownDuration;
                if (!CheckProximity(currentTarget))
                    state = EnemyState.Walk;
            }
        }
    }

    bool CheckProximity(Transform target)
    {
        return (Vector2.Distance(transform.position, currentTarget.transform.position) < 1.5f);
    }
}
