using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class entity : MonoBehaviour
{
    protected float movementSpeed = 6;

    protected float maxHealth;
    protected float health;

    protected const float GRAVITY = 2000f;

    public bool Dead {
        get { return health <= 0; }
    }
}
