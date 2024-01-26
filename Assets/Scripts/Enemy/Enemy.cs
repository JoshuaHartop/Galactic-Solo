using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private int _HP;
    private bool _inv = false;
    private Rigidbody2D _rb;
    private float _EnemyVelocity = 0f;
    private float _EnemyUpVelocity = 0f;
    private EnemySpawn spawner;
    private PointsScript points;
    private int _PointWorth;
    protected virtual void Start()
    {
        spawner = EnemySpawn.Instance;
        points = PointsScript.Instance;
    }

    public int PointWorth
    {
        get
        {
            return _PointWorth;
        }

        set
        {
            _PointWorth = value;
        }
    }
    public float EnemyUpVelocity
    {
        get 
        { 
            return _EnemyUpVelocity; 
        }
        set 
        { 
            _EnemyUpVelocity = value; 
        }
    }
    public bool INV
    {
        get 
        { 
            return _inv; 
        }

        set 
        { 
            _inv = value; 
        }
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
        if (_HP <= 0 && _inv == false)
        {
            Die();
        }
    }

    private void FixedUpdate()
    {
        if (_rb)
        {
            _rb.velocity = new Vector2(EnemyVelocity, EnemyUpVelocity);
        }
        
    }

    protected void Die()
    {
        
        Destroy(this.gameObject);
        
    }

    private void OnDestroy()
    {
        points.addPoints(_PointWorth);
        spawner.enemyDeath();
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
