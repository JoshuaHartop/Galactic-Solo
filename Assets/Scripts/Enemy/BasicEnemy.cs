using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemy : Enemy
{
    
    private Player player;
    protected override void Start()
    {
        base.Start();
        HP = 1;
        EnemyVelocity = -2.5f;
        RB = GetComponent<Rigidbody2D>();
        PointWorth = 100;

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Destroy(this.gameObject);
            player = collision.GetComponent<Player>();
            player.takeDamage(HP);
        }
    }

}

