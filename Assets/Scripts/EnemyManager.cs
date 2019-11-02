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

    public void AddRoom(Room room)
    {
        if(room != null)
            rooms.Add(room);
    }

    public void UpdateEnemyList()
    {
        foreach(Room room in rooms)
        {
            List<EnemySpawner> spawners = room.GetSpawners();
            foreach(EnemySpawner spawn in spawners)
            {
                List<Enemy> enemies = spawn.GetEnemies();
                foreach(Enemy en in enemies)
                {
                    if(!allEnemies.Contains(en) && en != null)
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

    public void RemoveEnemies()
    {
        for(int i = 0; i < allEnemies.Count; i++)
        {
            if(allEnemies[i].IsDead())
            {
                Destroy(allEnemies[i]);
            }
        }
    }

    public Enemy GetCollidingEnemy(Player player)

    {
        foreach (Enemy enemy in allEnemies)
        {
            if ((enemy).isCollidingWith(player))
            {
                return enemy;
            }
        }
        return null;
    }

    public void HandleCollisions(Player player)
    {
        Enemy colliding = GetCollidingEnemy(player);

        if (colliding != null)
        {
            player.TakeDamage(colliding.Damage);
        }
    }
}
