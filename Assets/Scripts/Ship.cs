using UnityEngine;

public class Ship : MonoBehaviour
{
    new Circle collider;
    float accelRate;                     // Small, constant rate of acceleration
    Vector3 vehiclePosition;             // Local vector for movement calculation
    Vector3 direction;                   // Way the vehicle should move
    Vector3 velocity;                    // Change in X and Y
    Vector3 acceleration;                // Small accel vector that's added to velocity
    float angleOfRotation;               // 0 
    float maxSpeed;                      // 0.5 per frame, limits mag of velocity
    float cooldown;
    bool shot;
    float buffer;
    bool invincible;
    int lives;
    bool tripleShot;
    float timer;

    void Start()
    {
        vehiclePosition = new Vector3(0, 0, 0);
        direction = new Vector3(1, 0, 0);
        velocity = new Vector3(0, 0, 0);
        cooldown = 0f;
        buffer = 0f;
        invincible = false;
        shot = false;
        accelRate = 0.006f;
        maxSpeed = 0.2f;
        lives = 3;
        timer = 0f;
        tripleShot = false;

        collider = GetComponentInChildren<Circle>();
    }

    void Update()
    {
        if(shot)
        {
            cooldown += Time.deltaTime;
        }
        if(cooldown >= 0.4)
        {
            cooldown = 0;
            shot = false;
        }

        if (invincible)
        {
            buffer += Time.deltaTime;
        }
        if(buffer >= 1f)
        {
            buffer = 0;
            invincible = false;
        }

        if (tripleShot)
        {
            timer += Time.deltaTime;
        }
        if (timer >= 8f)
        {
            timer = 0;
            tripleShot = false;
        }
    }

    public bool Shot
    {
        get
        {
            return shot;
        }
        set
        {
            shot = value;
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
            return vehiclePosition;
        }
    }

    public Circle Collider
    {
        get
        {
            return collider;
        }
    }

    public bool Invincible
    {
        get
        {
            return invincible;
        }
        set
        {
            invincible = value;
        }
    }

    public int Lives
    {
        get
        {
            return lives;
        }
        set
        {
            lives = value;
        }
    }

    public bool TripleShot
    {
        get
        {
            return tripleShot;
        }
        set
        {
            tripleShot = value;
        }
    }

    public void RotateVehicle()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            angleOfRotation += 1.5f;
            direction = Quaternion.Euler(0, 0, 1.5f) * direction;
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            angleOfRotation -= 1.5f;
            direction = Quaternion.Euler(0, 0, -1.5f) * direction;
        }
    }

    public void Drive()
    {
        // Accelerate
        if (Input.GetKey(KeyCode.UpArrow))
        {
            acceleration = accelRate * direction;
            velocity += acceleration;
        }
        else
        {
            velocity *= 0.98f;
        }

        velocity = Vector3.ClampMagnitude(velocity, maxSpeed);

        vehiclePosition += velocity;
    }

    public void SetTransform()
    {
        transform.rotation = Quaternion.Euler(0, 0, angleOfRotation);

        if (vehiclePosition.x < -55.6f || vehiclePosition.x > 55.6)
        {
            vehiclePosition = new Vector3(-vehiclePosition.x, vehiclePosition.y, vehiclePosition.z);
        }
        if (vehiclePosition.y < -31.25 || vehiclePosition.y > 31.25)
        {
            vehiclePosition = new Vector3(vehiclePosition.x, -vehiclePosition.y, vehiclePosition.z);
        }

        transform.position = vehiclePosition;
    }
}
