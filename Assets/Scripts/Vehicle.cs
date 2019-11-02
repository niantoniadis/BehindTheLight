using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vehicle : MonoBehaviour
{
    Vector3 position;
    Vector3 velocity;
    Vector3 desiredVelocity;
    Vector3 acceleration;
    float maxSpeed;
    float rotation;
    int mass;
    bool seeking;

    // Start is called before the first frame update
    void Start()
    {
        seeking = true;
        mass = 10;

        maxSpeed = 5f;

        velocity = Vector3.zero;
        acceleration = Vector3.zero;
        position = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (position.x < -8.887f + GetComponent<SpriteRenderer>().bounds.size.x/2)
        {
            acceleration.x = -acceleration.x;
            velocity.x = -velocity.x;
        }
        if (position.x > 8.887f - GetComponent<SpriteRenderer>().bounds.size.x / 2)
        {
            acceleration.x = -acceleration.x;
            velocity.x = -velocity.x;
        }
        if (position.y < -5f + GetComponent<SpriteRenderer>().bounds.size.y / 2)
        {
            acceleration.y = -acceleration.y;
            velocity.y = -velocity.y;
        }
        if (position.y > 5f - GetComponent<SpriteRenderer>().bounds.size.y / 2)
        {
            acceleration.y = -acceleration.y;
            velocity.y = -velocity.y;
        }

        velocity += acceleration * Time.deltaTime;
        position += velocity * Time.deltaTime;

        transform.rotation = Quaternion.Euler(0, 0, rotation - 90f);
        transform.position = position;

        acceleration = Vector3.zero;
    }

    public bool Seeking
    {
        get
        {
            return seeking;
        }
        set
        {
            seeking = value;
        }
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

    public bool IsColliding(SpriteRenderer colliding)
    {
        if (colliding.bounds.size.x/2 + GetComponent<SpriteRenderer>().bounds.size.x/2 > Vector3.Distance(colliding.bounds.center, GetComponent<SpriteRenderer>().bounds.center))
        {
            return true;
        }
        return false;
    }

    public void Teleport()
    {
        position = new Vector3(Random.Range(-8.5f, 8.5f), Random.Range(-4.5f, 4.5f), 0);
        transform.position = position;
    }

    public void SetRotation(Vehicle target)
    {
        Vector3 direction;
        if (seeking)
        {
            direction = target.position - position;
            direction.Normalize();
            rotation = Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg;
        }
        else
        {
            direction = position - target.position;
            direction.Normalize();
            rotation = Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg;
        }
    }
}
