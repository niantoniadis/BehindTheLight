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

    }

    void updateEnemyList()
    {
        foreach(Room room in rooms)
        {
            List<EnemySpawner> spawners = room.GetSpawners();
            foreach(EnemySpawner spawn in spawners)
            {
                List<Enemy> enemies = spawn.GetEnemies();
                foreach(Enemy en in enemies)
                {
                    if(!allEnemies.Contains(en))
                        allEnemies.Add(en);
                } 
            }    
        }
    }

    public void MoveEnemy(Player player)
    {
        foreach(Enemy enemy in allEnemies)
        {
            enemy.Move(player);
        }
    }
}
