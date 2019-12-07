using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyState { Walk, Attack }

public class Enemy : entity
{
    protected EnemyState state;
    public bool flying;

    
    void Start()
    {
        state = EnemyState.Walk;
        jumping = true;
    }

    public override void Update()
    {
        //base.Update();
        HandleStates();
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

    }

    void Attack()
    {
        
    }
}
