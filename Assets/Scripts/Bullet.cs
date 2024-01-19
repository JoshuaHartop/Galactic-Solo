using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Enemy enemy;
    private float moveSpeed = 0.05f;
    private Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Update()
    {
        transform.Translate(Vector2.right * moveSpeed);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "BulletBorder")
        {
            Destroy(this.gameObject);
        }

        else if (collision.tag == "Enemy")
        {
            enemy = collision.GetComponent<Enemy>();
            enemy.takeDamage(1);
        }
    }
}
