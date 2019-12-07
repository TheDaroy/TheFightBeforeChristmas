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

    public BezierCurve curve;
    public float bezierTime = 0;

    private float attackRange;

    public override void Start()
    {
        base.Start();
        state = EnemyState.Walk;
        jumping = true;
        currentTarget = towerTarget;
        bezierTime = 0;
        if (flying)
            transform.position = BezierPosition(curve.startPoint, curve.startTangent, curve.endTangent, curve.endPoint, bezierTime);
        if (GetComponentInChildren<CircleCollider2D>())
            attackRange = GetComponentInChildren<CircleCollider2D>().radius;
        else
            attackRange = 1.5f;
    }

    public override void Update()
    {
        //base.Update();

        if (attackCooldownTime > 0)
            attackCooldownTime -= Time.deltaTime;
        HandleStates();
        if (bezierTime > 1)
            bezierTime = 1;
        else if (bezierTime < 0)
            bezierTime = 0;
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
        if (!flying)
        {
            Vector3 move = transform.position;
            if (transform.position.x < currentTarget.position.x)
            {
                move += (Vector3.right * movementSpeed * Time.deltaTime);

                if (move.x > currentTarget.position.x)
                    move = new Vector2(currentTarget.position.x, move.y);

            }
            else if (transform.position.x > currentTarget.position.x)
            {
                move += (Vector3.left * movementSpeed * Time.deltaTime);

                if (move.x < currentTarget.position.x)
                    move = new Vector2(currentTarget.position.x, move.y);

            }
            transform.position = move;
        }
        else
        {
            bezierTime += movementSpeed * Time.deltaTime;

            transform.position = BezierPosition(curve.startPoint, curve.startTangent, curve.endTangent, curve.endPoint, bezierTime);
        }

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
        return (Vector2.Distance(transform.position, currentTarget.transform.position) < attackRange);
    }
    Vector3 BezierPosition(Vector3 s, Vector3 st, Vector3 et, Vector3 e, float t)
    {
        return (((-s + 3 * (st - et) + e) * t + (3 * (s + et) - 6 * st)) * t + 3 * (st - s)) * t + s;
    }

    public override void OnDeath()
    {
        Destroy(gameObject);
    }
}
