using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class entity : MonoBehaviour
{
    public float maxHealth = 100;
    protected float health;
    public int index;
    public virtual void Start()
    {
        health = maxHealth;
    }

    public bool Dead {
        get { return health <= 0; }
    }

    public void ApplyDamage(float damage)
    {
        health -= damage;
        if (Dead)
            health = 0;
    }

}
