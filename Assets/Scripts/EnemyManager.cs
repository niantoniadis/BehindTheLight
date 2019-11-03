using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public Dictionary<Room, List<Enemy>> allEnemies = new Dictionary<Room, List<Enemy>>();
    float damageTimeCounter = 0f;
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
            if(allEnemies.ContainsKey(room))
                roomEnemies = allEnemies[room];
            else
            {
                roomEnemies = new List<Enemy>();
                allEnemies.Add(room, roomEnemies);
            }
            foreach(EnemySpawner spawn in spawners)
            {
                enemies = spawn.GetEnemies();
                
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
        Enemy enemy;
        foreach(Room room in rooms)
        {
            for(int i = 0; i < allEnemies[room].Count; i++)
            {
                enemy = allEnemies[room][i];
                if(enemy.IsDead())
                {
                    allEnemies[room].Remove(enemy);
                    Destroy(enemy.gameObject);
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
                HandleCollisions(player, enemy);
            }
            foreach(Enemy enemy2 in allEnemies[room])
            {
                if(enemy != enemy2 && enemy.IsCollidingWith(enemy2.GetComponents<CircleCollider2D>()))
                {
                    enemy.ApplyForce(enemy.Position - enemy2.Position);
                    enemy2.ApplyForce(enemy2.Position - enemy.Position);
                }
            }
        }
    }

    public void SwordCollisions(Player player, Room room)
    {
        if (player.Attacking)
        {
            foreach (Enemy enemy in allEnemies[room])
            {
                CircleCollider2D[] attack = new CircleCollider2D[1];
                attack[0] = player.GetComponentInChildren<CircleCollider2D>();

                if(enemy.IsCollidingWith(attack))
                {
                    enemy.TakeDamage(player.Damage);
                    enemy.TakeKnockback(player);
                }
            }
        }
    }


    public void HandleCollisions(Player player, Enemy enemy)
    {
        damageTimeCounter += Time.deltaTime;
        
        if(damageTimeCounter >= 1)
        {
            player.TakeDamage(enemy.Damage);
            player.TakeKnockback(enemy);
            damageTimeCounter = 0;
        }
    }
}
