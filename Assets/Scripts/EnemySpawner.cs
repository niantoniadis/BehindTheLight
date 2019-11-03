using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public List<Enemy> enemies = new List<Enemy>();
    public Enemy enemy;
    bool active = false;
    public float counter = 0;
    float enemyFrequency = 6f;
    int maxEnemies = 1;

    public bool Active
    {
        get
        {
            return active;
        }
        set
        {
            active = value;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        SpawnEnemy();
    }

    public void SpawnEnemy()
    {
        counter += Time.deltaTime;
        if(counter >= enemyFrequency && enemies.Count < maxEnemies && active)
        {
            counter = 0;
            Vector3 pos = transform.position;
            enemies.Add(Instantiate(enemy.gameObject, pos, Quaternion.identity).GetComponent<Enemy>());
        }
    }

    public List<Enemy> GetEnemies()
    {
        return enemies;
    }
}
