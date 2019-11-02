using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public List<Room> rooms;
    public List<Enemy> allEnemies;
    // Start is called before the first frame update
    void Start()
    {
        rooms = new List<Room>();
        allEnemies = new List<Enemy>();
    }
    
    // Update is called once per frame
    void Update()
    {
        foreach(Room room in rooms)
        {
            List<EnemySpawner> spawners = room.getSpawners();
            foreach(EnemySpawner spawn in spawners)
            {
                List<Enemy> enemies = spawn.getEnemies();
                foreach(Enemy en in enemies)
                {
                    if(!allEnemies.Contains(en))
                        allEnemies.Add(en);
                } 
            }    
        }    
    }
}
