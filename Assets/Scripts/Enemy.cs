using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyType
{
    BABY, LURKER, BIGGESTBRAINIST
}

public class Enemy : Vehicle
{
    protected EnemyType behavior;
    float damageCounter = 0;
    // Start is called before the first frame update
    void Start()
    {
        direction = new Vector3(1, 0, 0);
        mass = 1;
        MAX_SPEED = 6f;
        int type = Random.Range(1, 3);
        if(type == 1)
            behavior = EnemyType.BABY;
        else if(type == 2) 
            behavior = EnemyType.LURKER;
        else
            behavior = EnemyType.BIGGESTBRAINIST;

        velocity = Vector3.zero;
        acceleration = Vector3.zero;
        position = transform.position;
        
        //default testing
        ACCELERATION_SCALE = 1.5f;
        health = 10;
        maxHealth = 12;
        damage = 3;
        knockback = 18;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Move(Player player)
    {
        CircleCollider2D[] check = new CircleCollider2D[1];
        check[0] = player.light;
        if(behavior == EnemyType.BABY)
        {
            if(!IsCollidingWith(check))
            {
                Seek(player);
                Movement();
            }
        }
        if(behavior == EnemyType.LURKER)
        {
            if(!IsCollidingWith(check)) 
            {
                ACCELERATION_SCALE = 4.0f;
            }
            else
                ACCELERATION_SCALE = 0.3f;
            Seek(player);
            Movement();
        }
        else
        {
            SeekAhead(player);
        }

    }
}
