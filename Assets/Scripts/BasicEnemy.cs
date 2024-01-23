using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemy : Enemy
{
    private Player player;
    void Start()
    {
        HP = 1;
        EnemyVelocity = 2.5f;
        RB = GetComponent<Rigidbody2D>();

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

