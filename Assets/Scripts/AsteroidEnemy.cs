using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidEnemy : Enemy
{
    private GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        base.Start();
        HP = -1;
        EnemyVelocity = -5f;
        player = GameObject.Find("Player");
        RB = GetComponent<Rigidbody2D>();
        INV = true;

    }

    // Update is called once per frame
 

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Destroy(player);
        }
    }
}
