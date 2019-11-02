﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Vehicle : MonoBehaviour
{
    protected Vector3 direction;
    protected Vector3 position;
    protected Vector3 velocity;
    protected Vector3 desiredVelocity;
    protected Vector3 acceleration;
    protected float rotation;
    protected int mass;
    protected int maxHealth;
    protected int health;
    protected int damage;
    protected float knockBack;

    protected float ACCELERATION_SCALE = 1;
    protected float MAX_SPEED;
    protected float FRICTION_COEF;

    // Start is called before the first frame update
    void Start()
    {
        direction = Vector3.zero;
        velocity = Vector3.zero;
        acceleration = Vector3.zero;
        position = transform.position;

        mass = 1;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int Damage
    {
        get
        {
            return damage;
        }
    }

    public Vector3 Direction
    {
        get
        {
            return direction;
        }
    }

    public Vector3 Position
    {
        get
        {
            return position;
        }
    }
    public Vector3 Velocity
    {
        get
        {
            return velocity;
        }
    }

    public void Movement()
    {
        velocity += acceleration * Time.deltaTime * ACCELERATION_SCALE;
        if(acceleration.magnitude == 0)
        {

        }
        velocity = Vector3.ClampMagnitude(velocity, MAX_SPEED);
        position += velocity * Time.deltaTime;

        transform.rotation = Quaternion.Euler(0, 0, rotation);
        transform.position = position;

        acceleration = Vector3.zero;
    }

    public void ApplyForce(Vector3 force)
    {
        acceleration += force / mass;
    }

    public void SetVelocity(Vector3 vel)
    {
        velocity = vel;
    }

    public void Flee(Vehicle target)
    {
        desiredVelocity = position - target.position;
        desiredVelocity = Vector3.ClampMagnitude(desiredVelocity, MAX_SPEED);
        acceleration += (desiredVelocity - velocity) / mass;
    }

    public void Seek(Vehicle target)
    {
        desiredVelocity = target.position - position;
        desiredVelocity = Vector3.ClampMagnitude(desiredVelocity, MAX_SPEED);
        acceleration += ((desiredVelocity - velocity) / mass);
    }

    public void SeekAhead(Vehicle target)
    {
        desiredVelocity = (target.position + velocity * 1.1f) - position;
        desiredVelocity = Vector3.ClampMagnitude(desiredVelocity, MAX_SPEED);
        acceleration += ((desiredVelocity - velocity) / mass);

    }

    public void ApplyFriction(float coef)
    {
        Vector3 friction = -1 * velocity.normalized;
        if((acceleration.x > 0 && (acceleration + friction * coef / mass).x < 0) || (acceleration.x > 0 && (acceleration + friction * coef / mass).x < 0))
        {
            acceleration.x = 0;
        }
        if ((acceleration.y > 0 && (acceleration + friction * coef / mass).y < 0) || (acceleration.y > 0 && (acceleration + friction * coef / mass).y < 0))
        {
            acceleration.y = 0;
        }
    }

    public bool isCollidingWith(Vehicle check)
    {
        foreach(CircleCollider2D collider in GetComponents<CircleCollider2D>())
        {
            foreach (CircleCollider2D checkCollider in check.GetComponents<CircleCollider2D>())
            {
                if (collider.IsTouching(GetComponent<CircleCollider2D>()))
                {
                    return true;
                }
            }
        }
        return false;
    }
    public void TakeDamage(int damage)
    {
        health -= damage;
    }

    public void Heal(int heal)
    {
        health += heal;
    }

    public bool IsDead()
    {
        return health <= 0;
    }
}   
