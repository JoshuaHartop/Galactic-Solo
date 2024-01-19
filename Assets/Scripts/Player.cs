using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{ 

    public float playerSpeed; // modifies player speed value
    private Rigidbody2D rb; 
    private Vector2 playerDirection;
    public GameObject bullet;

    private int bulletUpgrade = 1;
    private float attackCD = 1f; // float to describe attack cooldown

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float directionY = Input.GetAxisRaw("Vertical"); // gets the value if the player is pressing W or S
        float directionX = Input.GetAxisRaw("Horizontal"); // gets the value if the player is pressing A or D
        playerDirection = new Vector2(directionX, directionY).normalized; // sets the player to "look" and the direction of the button(s) being pressed

        if (Input.GetKeyDown("space"))
        {
            if (attackCD == 0)
            {
                spawnBullet();
                resetAttack();
            }
            
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
            Vector2 pos = new Vector2(transform.position.x, transform.position.y - (bulletUpgrade / 2f) + i * 1f + .5f);
            Instantiate(bullet, pos, transform.rotation); // spawning the bullet prefab infront of the player with the same rotation
        }

    }

    void resetAttack()
    {
        
    }

}
