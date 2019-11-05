using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    Enemy baby;
    Enemy lurker;
    Enemy bigBrain;
    Enemy coward;

    List<Enemy> enemies = new List<Enemy>();
    public Enemy enemy;
    bool active = false;
    float enemyFrequency = 6f;
    int maxEnemies = 1;
    float counter = 0;

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
        baby = Resources.Load<Enemy>("Baby");
        lurker = Resources.Load<Enemy>("Lurker");
        bigBrain = Resources.Load<Enemy>("BigBrain");
        coward = Resources.Load<Enemy>("Coward");
    }

    // Update is called once per frame
    void Update()
    {
        SpawnEnemy();
    }

    public void SpawnEnemy()
    {
        counter += Time.deltaTime;

        if (counter >= enemyFrequency && enemies.Count < maxEnemies && active)
        {
            int type = Random.Range(0, 4);
            counter = 0;

            switch (type)
            {
                case 0:
                    enemies.Add(Instantiate(baby.gameObject, transform.position, Quaternion.identity).GetComponent<Enemy>());
                    enemies[enemies.Count - 1].Behavior = EnemyType.BABY;
                    break;

                case 1:
                    enemies.Add(Instantiate(bigBrain.gameObject, transform.position, Quaternion.identity).GetComponent<Enemy>());
                    enemies[enemies.Count - 1].Behavior = EnemyType.BIGGESTBRAINIST;
                    break;

                case 2:
                    enemies.Add(Instantiate(coward.gameObject, transform.position, Quaternion.identity).GetComponent<Enemy>());
                    enemies[enemies.Count - 1].Behavior = EnemyType.COWARD;
                    break;

                case 3:
                    enemies.Add(Instantiate(lurker.gameObject, transform.position, Quaternion.identity).GetComponent<Enemy>());
                    enemies[enemies.Count - 1].Behavior = EnemyType.LURKER;
                    break;
            }
        }
    }

    public void ClearEnemyList()
    {
        for(int i = 0; i < enemies.Count; i++)
        {
            if(enemies[i] == null)
                enemies.RemoveAt(i);
        }
    }

    public List<Enemy> GetEnemies()
    {
        return enemies;
    }
}
