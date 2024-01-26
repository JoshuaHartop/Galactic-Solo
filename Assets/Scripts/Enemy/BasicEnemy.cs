using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

[RequireComponent(typeof(OutOfBoundsListener))]
public class BasicEnemy : Enemy
{
    private Player player;

    private Camera _mainCamera;

    protected override void Start()
    {
        base.Start();
        HP = 1;
        EnemyVelocity = -2.5f;
        RB = GetComponent<Rigidbody2D>();
        PointWorth = 100;

        _mainCamera = Camera.main;
        GetComponent<OutOfBoundsListener>().onOutOfBounds += OnOutOfBounds;
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

    private void OnOutOfBounds(OutOfBoundsListener.BoundsDirection direction)
    {
        if (direction == OutOfBoundsListener.BoundsDirection.West)
        {
            // Move to the east side of the viewport bounds
            transform.position = new Vector3(
                _mainCamera.transform.position.x + (_mainCamera.orthographicSize * _mainCamera.aspect) + 1f,
                transform.position.y, 
                transform.position.z
            );
        }
    }

}

