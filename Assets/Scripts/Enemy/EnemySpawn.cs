using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class EnemySpawn : LocalManager<EnemySpawn>
{
    [SerializeField] private Enemy basicEnemy;
    [SerializeField] private Enemy shooterEnemy;
    [SerializeField] private Enemy Asteroid;
    public WaveTextScript textscript;
    
    private float minY = -6;
    private float maxY = 6;
    private int _CurrentWave = 2;
    private int _CurrentEnemyCount;
    public int CurrentEnemyCount
    {
        get 
        { 
            return _CurrentEnemyCount;
        }
        set 
        { 
            _CurrentEnemyCount = value;
        }
    }

    protected override bool Persistent => false;

    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        if (CurrentEnemyCount == 0)
        {
            _CurrentWave++;
            spawnEnemies();
        }
    }

    void spawnEnemies()
    {
        textscript.textAppear();
        textscript.setText("Wave " + _CurrentWave);
        print("spawned");
        for (int i = 0; i < _CurrentWave; i++) // spawner for basic
        {
            float randomY = Random.Range(minY, maxY);
            Vector2 pos = new Vector2(transform.position.x, randomY);
            Instantiate(basicEnemy, basicEnemy.transform.position + (Vector3)pos, basicEnemy.transform.rotation);
            CurrentEnemyCount++;
        }
        for (int i = 0; i < (_CurrentWave / 2); i++) // spawner for shooter
        {
            float randomY = Random.Range(minY, maxY);
            Vector2 pos = new Vector2(12.75f, randomY);
            Instantiate(shooterEnemy, shooterEnemy.transform.position + (Vector3)pos, basicEnemy.transform.rotation);
            CurrentEnemyCount++;

        }
        for (int i = 0; i < (_CurrentWave / 4); i++) // spawner for asteroid
        {
            float randomY = Random.Range(minY, maxY);
            Vector2 pos = new Vector2(transform.position.x, randomY);

            Instantiate(Asteroid, pos, transform.rotation);
            CurrentEnemyCount++;

        }
    }

    public void enemyDeath()
    {
        CurrentEnemyCount--;
    }
}
