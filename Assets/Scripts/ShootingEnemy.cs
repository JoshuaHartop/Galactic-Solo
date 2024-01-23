using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingEnemy : Enemy
{
    public GameObject missile;
    private float attackCD = 1;
    private float lastAttackTime;
    // Start is called before the first frame update
    void Start()
    {
        HP = 2;
        
    }

    protected override void Update()
    {
        base.Update();
        if (Time.time > lastAttackTime + attackCD)
        {
            Shoot();
            lastAttackTime = Time.time;
        }

    }

    private void Shoot()
    {
        Vector2 pos = new Vector2(transform.position.x, transform.position.y);
        Instantiate(missile, pos, transform.rotation);
    }
}
