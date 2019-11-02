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
    protected float maxSpeed;
    protected float rotation;
    protected int mass;
    protected float maxHealth;
    protected float health;
    protected float damage;

    // Start is called before the first frame update
    void Start()
    {
        mass = 10;

        maxSpeed = 5f;

        velocity = Vector3.zero;
        acceleration = Vector3.zero;
        position = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Movement()
    {
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
        desiredVelocity = Vector3.ClampMagnitude(desiredVelocity, maxSpeed);
        acceleration += (desiredVelocity - velocity) / mass;
    }

    public void Seek(Vehicle target)
    {
        desiredVelocity = target.position - position;
        desiredVelocity = Vector3.ClampMagnitude(desiredVelocity, maxSpeed);
        acceleration += ((desiredVelocity - velocity) / mass);
    }
}
