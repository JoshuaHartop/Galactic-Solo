using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    private Player player;
    private float _BulletVelocity;
    private Rigidbody2D rb;

    public float BulletVelocity
    {
        get
        {
            return _BulletVelocity; 
        }
        set
        {
            _BulletVelocity = value;
        }
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        if (_BulletVelocity == 0)
        {
            _BulletVelocity = 5.00f;
        }
    }

    private void FixedUpdate()
    {
        rb.velocity = (Vector2.left * _BulletVelocity);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "BulletBorder")
        {
            Destroy(this.gameObject);
        }

        else if (collision.tag == "Player")
        {
            player = collision.GetComponent<Player>();
            player.takeDamage(1);
            Destroy(this.gameObject);
           
        }
    }


}
