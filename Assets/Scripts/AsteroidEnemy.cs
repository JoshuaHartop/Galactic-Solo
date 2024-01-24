using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AsteroidEnemy : Enemy
{
    [SerializeField]
    private float _rotationSpeedMin;

    [SerializeField]
    private float _rotationSpeedMax;

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
    override protected void Update()
    {
        base.Update();

        float rotationSpeed = Random.Range(_rotationSpeedMin, _rotationSpeedMax);
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles + Vector3.forward * rotationSpeed * Time.deltaTime);
    }
 

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Destroy(player);
        }
    }
}
