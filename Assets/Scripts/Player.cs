using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

// Todo: Actually implement
public class PlayerData : SaveData<PlayerData>
{
    public int health;
    public float attackInterval;
    public int bulletCount;
}

[RequireComponent(typeof(OutOfBoundsListener))]
public class Player : MonoBehaviour
{ 
    [SerializeField]
    private Transform _bulletSpawnPoint;

    public float playerSpeed; // modifies player speed value
    private Rigidbody2D rb; 
    private Vector2 playerDirection;
    public GameObject bullet;
    private float lastAttackTime; // number to get last time the space was pressed

    [SerializeField]
    private int _HP;
    public int _BulletUpgrade = 1; // number for how many bullets to shoot
    private float attackCD = 1f; // float to describe attack cooldown (smaller = attack more often)

    private Camera _mainCamera;

    private OutOfBoundsListener.BoundsDirection _outOfBoundsDirectionFlags;

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

    // Start is called before the first frame update
    void Start()
    {
        _mainCamera = Camera.main;

        rb = GetComponent<Rigidbody2D>();
        // if (_HP == 0)
        // {
        //     _HP = 1;
        // }

        GetComponent<OutOfBoundsListener>().onOutOfBounds += OnOutOfBounds;
    }

    // Update is called once per frame
    void Update()
    {
        float directionY = Input.GetAxisRaw("Vertical"); // gets the value if the player is pressing W or S
        float directionX = Input.GetAxisRaw("Horizontal"); // gets the value if the player is pressing A or D
        playerDirection = new Vector2(directionX, directionY).normalized; // sets the player to "look" and the direction of the button(s) being pressed

        if (Input.GetKey("space") && Time.time > lastAttackTime + attackCD)
        {
            spawnBullet();
            lastAttackTime = Time.time;
        }

        if (_HP <= 0)
        {
            Destroy(this.gameObject);
        }
        // do gameover here
    }

    void FixedUpdate()
    {
        Vector2 targetVelocity = playerDirection * playerSpeed;

        // Prevent player from going out of bounds
        if (_outOfBoundsDirectionFlags.HasFlag(OutOfBoundsListener.BoundsDirection.North))
        {
            if (targetVelocity.y > 0f)
                targetVelocity.y = 0f;
        }
        else if (_outOfBoundsDirectionFlags.HasFlag(OutOfBoundsListener.BoundsDirection.South))
        {
            if (targetVelocity.y < 0f)
                targetVelocity.y = 0f;
        }

        if (_outOfBoundsDirectionFlags.HasFlag(OutOfBoundsListener.BoundsDirection.East))
        {
            if (targetVelocity.x > 0f)
                targetVelocity.x = 0f;
        }
        else if (_outOfBoundsDirectionFlags.HasFlag(OutOfBoundsListener.BoundsDirection.West))
        {
            if (targetVelocity.x < 0f)
                targetVelocity.x = 0f;
        }

        rb.velocity = targetVelocity; // moves the player in the direction of the button(s) being pressed
    }

    void spawnBullet()
    {

        for (int i = 0; i < _BulletUpgrade; i++)
        {
            Vector2 pos = new Vector2(_bulletSpawnPoint.position.x, _bulletSpawnPoint.transform.position.y - (_BulletUpgrade / 2f) + i * 1f + .5f);

            // pos += new Vector2(_bulletSpawnPoint.position.x, _bulletSpawnPoint.transform.position.y);

            // Keep the rotation set in the bullet prefab
            Instantiate(bullet, bullet.transform.position + (Vector3)pos, bullet.transform.rotation); // spawning the bullet prefab infront of the player with the same rotation
        }

    }

    public void takeDamage(int damage)
    {
        _HP = _HP - damage;
    }

    private void OnOutOfBounds(OutOfBoundsListener.BoundsDirection direction)
    {
        _outOfBoundsDirectionFlags = direction;
    }

}
