using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    public EnemySpawner spawner;
    public List<EnemySpawner> spawners;
    public GameObject background;
    // Start is called before the first frame update
    void Start()
    {
        spawners = new List<EnemySpawner>();
        background = Instantiate(background.gameObject, background.transform.position, Quaternion.identity);
        SpawnSpawners();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SpawnSpawners()
    {
        float right = background.GetComponent<SpriteRenderer>().bounds.max.x;
        float left = background.GetComponent<SpriteRenderer>().bounds.min.x;
        float top = background.GetComponent<SpriteRenderer>().bounds.max.y;
        float bottom = background.GetComponent<SpriteRenderer>().bounds.min.y;
        spawners.Add(Instantiate(spawner.gameObject, new Vector3(0.9f * right, top + bottom, 0), Quaternion.identity).GetComponent<EnemySpawner>());
        spawners.Add(Instantiate(spawner.gameObject, new Vector3(0.9f * left, top + bottom, 0), Quaternion.identity).GetComponent<EnemySpawner>());
        spawners.Add(Instantiate(spawner.gameObject, new Vector3(right + left, 0.9f * top, 0), Quaternion.identity).GetComponent<EnemySpawner>());
        spawners.Add(Instantiate(spawner.gameObject, new Vector3(right + left, 0.9f * bottom, 0), Quaternion.identity).GetComponent<EnemySpawner>());
    }

    public List<EnemySpawner> GetSpawners()
    {
        return spawners;
    }

}
