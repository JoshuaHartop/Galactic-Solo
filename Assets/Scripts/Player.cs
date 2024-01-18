using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class Player : MonoBehaviour
{ 

    public float playerSpeed; // modifies player speed value
    private Rigidbody2D rb; 
    private Vector2 playerDirection;
    public GameObject bullet;
    private int bulletUpgrade;
    private float attackCD = 1f; // float to describe attack cooldown
    private int bulletUpDown;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        bulletUpgrade = 3;
        bulletUpDown = 1;
    }

    // Update is called once per frame
    void Update()
    {
        float directionY = Input.GetAxisRaw("Vertical"); // gets the value if the player is pressing W or S
        float directionX = Input.GetAxisRaw("Horizontal"); // gets the value if the player is pressing A or D
        playerDirection = new Vector2(directionX, directionY).normalized; // sets the player to "look" and the direction of the button(s) being pressed

        if (Input.GetKeyDown("space"))
        {

            spawnBullet();
            
        }
    }

    void FixedUpdate()
    {
        rb.velocity = playerDirection * playerSpeed; // moves the player in the direction of the button(s) being pressed
    }

    IEnumerator loopDelay()
    {
        yield return new WaitForSeconds(1);
    }

    void spawnBullet()
    {
         
        for (int i = 0; i < bulletUpgrade; i++)
        {
            bulletUpDown = -1 * bulletUpDown;
            Vector2 pos = new Vector2(transform.position.x + 1, bulletUpDown * (transform.position.y + i)/2); // setting pos to be slightly ahead of the player
            Instantiate(bullet, pos, transform.rotation); // spawning the bullet prefab infront of the player with the same rotation
            
        }

    }

}
