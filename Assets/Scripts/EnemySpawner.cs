using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public List<Enemy> enemies;
    public Enemy enemy;
    public float counter = 0;
    public int enemyFrequency = 3;
    public int maxEnemies = 5;
    // Start is called before the first frame update
    void Start()
    {
        enemies = new List<Enemy>();
    }

    // Update is called once per frame
    void Update()
    {
        SpawnEnemy();
    }

    void SpawnEnemy()
    {
        counter += Time.deltaTime;
        if(counter >= enemyFrequency && enemies.Count < maxEnemies)
        {
            counter = 0;
            enemies.Add(Instantiate(enemy.gameObject, transform.position, Quaternion.identity).GetComponent<Enemy>());
        }
    }

    public List<Enemy> getEnemies()
    {
        return enemies;
    }
}
