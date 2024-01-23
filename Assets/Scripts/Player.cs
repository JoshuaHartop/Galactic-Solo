using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{ 

    public float playerSpeed; // modifies player speed value
    private Rigidbody2D rb; 
    private Vector2 playerDirection;
    public GameObject bullet;
    private float lastAttackTime; // number to get last time the space was pressed
    private int _HP;
    public int _BulletUpgrade = 1; // number for how many bullets to shoot
    private float attackCD = 1f; // float to describe attack cooldown (smaller = attack more often)

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
        rb = GetComponent<Rigidbody2D>();
        if (_HP == 0)
        {
            _HP = 1;
        }
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
        rb.velocity = playerDirection * playerSpeed; // moves the player in the direction of the button(s) being pressed
    }

    void spawnBullet()
    {

        for (int i = 0; i < _BulletUpgrade; i++)
        {
            Vector2 pos = new Vector2(transform.position.x, transform.position.y - (_BulletUpgrade / 2f) + i * 1f + .5f);
            Instantiate(bullet, pos, transform.rotation); // spawning the bullet prefab infront of the player with the same rotation
        }

    }

    public void takeDamage(int damage)
    {
        _HP = -_HP - damage;
    }

}
