using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private int _HP;
    private Rigidbody2D _rb;
    private float _EnemyVelocity;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public Rigidbody2D RB
    {
        get
        {
            return _rb;
        }

        set
        {
            _rb = value;
        }
    }
    public int HP
    {
        get
        {
            return _HP;
        }

        set
        {
             _HP = value;
        }
    }
    public float EnemyVelocity
    {
        get
        {
            return _EnemyVelocity;
        }

        set
        {
            _EnemyVelocity = value;
        }
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if (_HP <= 0)
        {
            Die();
        }
    }

    private void FixedUpdate()
    {
        _rb.velocity = (Vector2.left * _EnemyVelocity);
    }

    protected void Die()
    {
        Destroy(this.gameObject);
    }

    public void takeDamage(int damage)
    {
        _HP = _HP - damage;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "EnemyBorder")
        {
            Destroy(this.gameObject);
        }
    }
}
