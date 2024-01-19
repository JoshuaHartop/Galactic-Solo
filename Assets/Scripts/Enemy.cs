using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private int HP = 1;
    // Start is called before the first frame update
    void Start()
    {
        
    }


    // Update is called once per frame
    void Update()
    {
        if (HP == 0)
        {
            Die();
        }
    }

    void Die()
    {
        Destroy(this.gameObject);
    }

     public void takeDamage(int damage)
    {
        HP = HP - damage;
    }
}
