using System.Collections;
using System.Collections.Generic;
using System.Security;
using UnityEngine;

[RequireComponent(typeof(OutOfBoundsListener))]
public class Bullet : MonoBehaviour
{
    [SerializeField]
    private AudioClip _collisionSound;

    private int _BulletPierce;
    private Enemy enemy;
    private float _BulletVelocity;
    private Rigidbody2D rb;

    public int BulletPierce
    {
        get 
        { 
            return _BulletPierce;
        } 
        set 
        {
            _BulletPierce = value;
        }
    }

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
        if (_BulletPierce == 0)
        {
            _BulletPierce = 1;
        }

        if (_BulletVelocity == 0)
        {
            _BulletVelocity = 5.00f;
        }

        GetComponent<OutOfBoundsListener>().onOutOfBounds += OnOutOfBounds;
    }

    private void FixedUpdate()
    {
        rb.velocity = (Vector2.right * _BulletVelocity);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            enemy = collision.GetComponent<Enemy>();
            enemy.takeDamage(1);
            _BulletPierce --;
            if (_BulletPierce <= 0)
            {
                Destroy(this.gameObject);
            }
        }

        else if (collision.tag == "EnemyBullet")
        {
            SoundManager.Instance.PlaySound(_collisionSound, 0.33f);

            Destroy(this.gameObject);
            Destroy(collision.gameObject);
        }
    }

    private void OnOutOfBounds(OutOfBoundsListener.BoundsDirection direction)
    {
        Destroy(gameObject);
    }
}
