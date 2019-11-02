using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public EnemySpawner spawner;
    public List<EnemySpawner> spawners;
    public List<Enemy> allEnemies;
    // Start is called before the first frame update
    void Start()
    {
        spawners = new List<EnemySpawner>();
        allEnemies = new List<Enemy>();
        spawners.Add(Instantiate(spawner.gameObject, new Vector3(10, 0, 0), Quaternion.identity).GetComponent<EnemySpawner>());
        spawners.Add(Instantiate(spawner.gameObject, new Vector3(-10, 0, 0), Quaternion.identity).GetComponent<EnemySpawner>());
        spawners.Add(Instantiate(spawner.gameObject, new Vector3(0, 10, 0), Quaternion.identity).GetComponent<EnemySpawner>());
        spawners.Add(Instantiate(spawner.gameObject, new Vector3(0, -10, 0), Quaternion.identity).GetComponent<EnemySpawner>());
    }

    // Update is called once per frame
    void Update()
    {
        foreach(EnemySpawner e in spawners)
        {

        }        
    }
}
