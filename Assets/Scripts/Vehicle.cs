using System.Collections;
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
    protected float maxHealth;
    protected float health;
    protected float damage;

    protected float ACCELERATION_SCALE = 1;
    protected float MAX_SPEED;
    protected float FRICTION_COEF;

    // Start is called before the first frame update
    void Start()
    {
        velocity = Vector3.zero;
        acceleration = Vector3.zero;
        position = transform.position;

        mass = 1;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public float Damage
    {
        get
        {
            return damage;
        }
    }

    public void Movement()
    {
        velocity += acceleration * Time.deltaTime * ACCELERATION_SCALE;
        Vector3.ClampMagnitude(velocity, MAX_SPEED);
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

    public void ApplyFriction(float coef)
    {
        Vector3 friction = -1 * velocity.normalized;
        acceleration += friction * coef / mass;
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
}
