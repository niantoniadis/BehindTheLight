using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public Dictionary<Room, List<Enemy>> allEnemies = new Dictionary<Room, List<Enemy>>();
    // Start is called before the first frame update
    void Start()
    { 

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
            List<Enemy> enemies;
            List<Enemy> roomEnemies;
            foreach(EnemySpawner spawn in spawners)
            {
                enemies = spawn.GetEnemies();
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
            if(enemy.IsCollidingWith(player.GetComponents<CircleCollider2D>()))
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
            CircleCollider2D[] check = new CircleCollider2D[1];
            check[0] = player.GetComponentInChildren<CircleCollider2D>();
            if(enemy.IsCollidingWith(check))
            {
                enemy.TakeDamage(player.Damage);
                enemy.TakeKnockback(player);
            }
        }
    }

    public void HandleCollisions(Player player, int damage)
    {
        Debug.Log("taking damage");
        player.TakeDamage(damage);
    }
}
