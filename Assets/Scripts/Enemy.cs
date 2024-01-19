using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private int _HP;
    // Start is called before the first frame update
    void Start()
    {
        
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

    // Update is called once per frame
    protected virtual void Update()
    {
        if (_HP <= 0)
        {
            Die();
        }
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
