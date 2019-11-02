using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public Dictionary<Room, List<Enemy>> allEnemies;
    // Start is called before the first frame update
    void Start()
    { 
        allEnemies = new Dictionary<Room, List<Enemy>>();
    }
    
    // Update is called once per frame
    void Update()
    {

    }

    public void UpdateEnemyList(List<Room> rooms)
    {
        foreach(Room room in rooms)
        {
            List<EnemySpawner> spawners = room.GetSpawners();
            foreach(EnemySpawner spawn in spawners)
            {
                List<Enemy> enemies = spawn.GetEnemies();
                List<Enemy> roomEnemies;
                if(allEnemies.ContainsKey(room))
                    roomEnemies = allEnemies[room];
                else
                {
                    roomEnemies = new List<Enemy>();
                    allEnemies.Add(room, roomEnemies);
                }
                foreach(Enemy en in enemies)
                {
                    if(!roomEnemies.Contains(en) && en != null)
                        allEnemies[room].Add(en);
                } 
            }    
        }
    }

    public void MoveEnemy(Player player, List<Room> rooms)
    {
        foreach(Room room in rooms)
        {
            foreach(Enemy enemy in allEnemies[room])
            {
                enemy.Move(player);
            }
        }
    }

    public void RemoveEnemies(List<Room> rooms)
    {
        foreach(Room room in rooms)
        {
            foreach(Enemy enemy in allEnemies[room])
            {
                if(enemy.IsDead())
                {
                    Destroy(enemy);
                }
            }
        }
    }

    public void EnemyCollisions(Player player, Room room)
    {
        foreach(Enemy enemy in allEnemies[room])
        {
            Debug.Log("Enemy:" + enemy.Position);
            Debug.Log("Player:" + player.Position);
            if(enemy.IsCollidingWith(player))
            {
                Debug.Log("69 lol");
                HandleCollisions(player, enemy.Damage);
            }
        }
    }

    public void SwordCollisions(Player player, Room room)
    {
        foreach(Enemy enemy in allEnemies[room])
        {
            if(enemy.IsCollidingWith(player.GetComponentInChildren<CircleCollider2D>()))
            {
                enemy.TakeDamage(player.Damage);
            }
        }
    }

    public void HandleCollisions(Player player, int damage)
    {
        Debug.Log("taking damage");
        player.TakeDamage(damage);
    }
}
