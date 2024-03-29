using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingEnemy : Enemy
{
    public GameObject missile;

    [SerializeField]
    private Transform _bulletSpawnPoint;

    private float attackCD = 2f;
    private float lastAttackTime;
    // Start is called before the first frame update
    void Start()
    {
        base.Start();
        EnemyUpVelocity = 2;
        HP = 2;
        RB = GetComponent<Rigidbody2D>();
        PointWorth = 500;

        attackCD += Random.Range(0f, 1f);
        lastAttackTime = Time.time;
    }

    protected override void Update()
    {
        base.Update();
        moveUpDown();
        if (Time.time > lastAttackTime + attackCD)
        {
            Shoot();
            lastAttackTime = Time.time;
        }
        

    }


    private void Shoot()
    {
        Vector2 pos = new Vector2(transform.position.x, transform.position.y);
        Instantiate(missile, _bulletSpawnPoint.position, missile.transform.rotation);
    }

    private void moveUpDown()
    {
        if (transform.position.y > 6.35)
        {
            EnemyUpVelocity = -2;
        }

        else if (transform.position.y < -6.35)
        {
            EnemyUpVelocity = 2;
        }

    }
}
