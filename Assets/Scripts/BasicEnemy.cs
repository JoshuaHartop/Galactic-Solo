using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemy : Enemy
{
    void Start()
    {
        HP = 1;
        EnemyVelocity = 2.5f;
        RB = GetComponent<Rigidbody2D>();
    }

}

