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
    // Start is called before the first frame update
    void Start()
    {
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
        ACCELERATION_SCALE = 1.5f;
        health = 10;
        maxHealth = 12;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Move(Player player)
    {
        if(behavior == EnemyType.BABY)
        {
            //if not light
            Seek(player);
        }
        if(behavior == EnemyType.LURKER)
        {
            //if dark 
            //{
                // ACCELERATION_SCALE = 3.0f;
            //}
            //else
                // ACCELERATION_SCALE = 0.5f;
            Seek(player);
        }
        if(behavior == EnemyType.BIGGESTBRAINIST)
        {
            SeekAhead(player);
        }
        Movement();
    }


}
