using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    public EnemySpawner spawner;
    public List<EnemySpawner> spawners;
    // Start is called before the first frame update
    void Start()
    {
        spawners = new List<EnemySpawner>();
        SpawnSpawners();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SpawnSpawners()
    {
        spawners.Add(Instantiate(spawner.gameObject, new Vector3(10, 0, 0), Quaternion.identity).GetComponent<EnemySpawner>());
        spawners.Add(Instantiate(spawner.gameObject, new Vector3(-10, 0, 0), Quaternion.identity).GetComponent<EnemySpawner>());
        spawners.Add(Instantiate(spawner.gameObject, new Vector3(0, 10, 0), Quaternion.identity).GetComponent<EnemySpawner>());
        spawners.Add(Instantiate(spawner.gameObject, new Vector3(0, -10, 0), Quaternion.identity).GetComponent<EnemySpawner>());
    }

    public List<EnemySpawner> getSpawners()
    {
        return spawners;
    }

}
