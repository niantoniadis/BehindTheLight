using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyType
{
    BABY, LURKER, COWARD, BIGGESTBRAINIST
}



public class Enemy : Vehicle
{
    public GameObject healthBar;
    protected EnemyType behavior;
    float attacked;
    float hitBuffer = 0.1f;
    // Start is called before the first frame update
    void Start()
    {
        direction = new Vector3(1, 0, 0);
        mass = 1;
        MAX_SPEED = 4f;
        int type = Random.Range(1, 4);
        if(type == 1)
            behavior = EnemyType.BABY;
        else if(type == 2) 
            behavior = EnemyType.LURKER;
        else if(type == 3)
            behavior = EnemyType.COWARD;
        else
            behavior = EnemyType.BIGGESTBRAINIST;

        velocity = Vector3.zero;
        acceleration = Vector3.zero;
        position = transform.position;
        
        //default testing
        ACCELERATION_SCALE = 1.5f;
        health = 12;
        maxHealth = 12;
        damage = 3;
        knockback = 18;
        healthBar.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public float Attacked
    {
        get
        {
            return attacked;
        }
        set
        {
            attacked = value;
        }
    }

    public float HitBuffer
    {
        get
        {
            return hitBuffer;
        }
    }

    public void Move(Player player)
    {
        CircleCollider2D[] check = new CircleCollider2D[1];
        check[0] = player.light;
        if(behavior == EnemyType.BABY)
        {
            if(!IsCollidingWith(check))
            {
                ACCELERATION_SCALE = 1.5f;
                Seek(player);
                Movement();
            }
        }
        if(behavior == EnemyType.LURKER)
        {
            if(!IsCollidingWith(check)) 
            {
                ACCELERATION_SCALE = 1.5f;
            }
            else
                ACCELERATION_SCALE = 0.4f;
            Seek(player);
            Movement();
        }
        if(behavior == EnemyType.COWARD)
        {
            if(IsCollidingWith(check))
            {
                ACCELERATION_SCALE = 1.5f;
                Flee(player.light.bounds.center);
            }
            else
            {
                ACCELERATION_SCALE = 1.2f;
                Seek(player);
            }
            Movement();
        }
        else // BIGGESTBRAINIST
        {
            SeekAhead(player);
            Movement();
        }

    }

    public void UpdateHealth()
    {
        healthBar.SetActive(health < maxHealth);
        if (health < maxHealth)
        {
            healthBar.transform.localScale = new Vector3((float)health / (float)maxHealth, healthBar.transform.localScale.y, 0);
        }
    }
}
