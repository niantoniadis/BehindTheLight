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
        int type = Random.Range(1, 5);

        velocity = Vector3.zero;
        acceleration = Vector3.zero;
        position = transform.position;
        
        //default testing
        ACCELERATION_SCALE = 1.5f;
        health = 12;
        maxHealth = 12;
        damage = 5;
        knockback = 18;
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

    public EnemyType Behavior
    {
        get
        {
            return behavior;
        }
        set
        {
            behavior = value;
        }
    }

    public void Move(Player player)
    {
        CircleCollider2D[] check = new CircleCollider2D[1];
        check[0] = player.lightCollider;
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
                Flee(player.lightCollider.bounds.center);
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

        RotateVehicle(player);
    }

    public void UpdateHealth()
    {
        if (health < maxHealth)
        {
            if (healthBar == null)
            {
                healthBar = Instantiate(healthBar, Vector3.zero, Quaternion.identity);
            }
            switch (behavior)
            {
                case EnemyType.BABY:
                    healthBar.transform.position = new Vector3(position.x - 0.43f, position.y + 0.765f);
                    healthBar.transform.localScale = new Vector3((float)health / (float)maxHealth, healthBar.transform.localScale.y, 0);
                    break;

                case EnemyType.BIGGESTBRAINIST:
                    healthBar.transform.position = new Vector3(position.x - 0.209f, position.y + 0.503f);
                    healthBar.transform.localScale = new Vector3((float)health / (float)maxHealth, healthBar.transform.localScale.y, 0);
                    break;

                case EnemyType.COWARD:
                    healthBar.transform.position = new Vector3(position.x - 0.387f, position.y + 0.476f);
                    healthBar.transform.localScale = new Vector3((float)health / (float)maxHealth, healthBar.transform.localScale.y, 0);
                    break;

                case EnemyType.LURKER:
                    healthBar.transform.position = new Vector3(position.x - 0.427f, position.y + 0.359f);
                    healthBar.transform.localScale = new Vector3((float)health / (float)maxHealth, healthBar.transform.localScale.y, 0);
                    break;
            }
            
        }
    }

    public void RotateVehicle(Player player)
    {
        float prevRotation = rotation;
        rotation = Mathf.Atan2(velocity.normalized.x, -velocity.normalized.y) * Mathf.Rad2Deg;
        direction = Quaternion.Euler(0, 0, rotation - prevRotation) * direction;
    }
}
