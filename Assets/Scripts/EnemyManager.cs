﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public Vehicle boid;
    public Dictionary<Room, List<Enemy>> allEnemies = new Dictionary<Room, List<Enemy>>();
    float damageTimeCounter = 0f;

    List<Vehicle> boids;

    // Start is called before the first frame update
    void Start()
    {
        boids = new List<Vehicle>();

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

    public Vector3 RemoveEnemies(List<Room> rooms)
    {
        Vector3 pos = Vector3.positiveInfinity;

        foreach(Room room in rooms)
        {
            for(int i = 0; i < allEnemies[room].Count; i++)
            {
                if(allEnemies[room][i].IsDead())
                {
                    pos = allEnemies[room][i].Position;
                    Destroy(allEnemies[room][i].gameObject);
                    allEnemies[room].RemoveAt(i);
                }
            }
            foreach(EnemySpawner spawner in room.GetSpawners())
            {
                spawner.ClearEnemyList();
            }
        }
        return pos;
    }

    public void EnemyCollisions(Player player, Room room)
    {
        foreach(Enemy enemy in allEnemies[room])
        {
            enemy.UpdateHealth();
            if (enemy.IsCollidingWith(player.GetComponents<CircleCollider2D>()))
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
                attack[0] = player.attack;

                if(enemy.IsCollidingWith(attack) && enemy.Attacked >= enemy.HitBuffer)
                {
                    enemy.TakeDamage(player.Damage);
                    enemy.TakeKnockback(player);
                    enemy.Attacked = 0f;
                }
                enemy.Attacked += Time.deltaTime;
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

    public void spawnBoids()
    {
        for(int i = 0; i < 6; i++)
        {
            boids.Add(Instantiate(boid.gameObject, new Vector3(Random.Range(-7.5f, 7.5f), Random.Range(-4f, 4f), 0), Quaternion.Euler(0, 0, Random.Range(0, 360))).GetComponent<Vehicle>());
        }
    }

    public void updateBoids()
    {

    }
}
