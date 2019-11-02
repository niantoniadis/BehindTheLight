using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Vehicle
{
    float maxStamina;
    float staminaResetBuffer;
    float stamina;

    // Start is called before the first frame update
    void Start()
    {
        ACCELERATION_SCALE = 30.0f;
        MAX_SPEED = 5f;
        FRICTION_COEF = 1.0f;
        health = 20;
        maxHealth = 20;
        mass = 1;
        maxStamina = 4f;
        stamina = maxStamina;
        staminaResetBuffer = 1.5f;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public float Stamina
    {
        get
        {
            return stamina;
        }
    }

    public float MaxStamina
    {
        get
        {
            return maxStamina;
        }
    }

    public float MaxHealth
    {
        get
        {
            return maxHealth;
        }
    }

    public float Health
    {
        get
        {
            return health;
        }
    }

    public void Move()
    {
        if (Input.GetKey(KeyCode.Space) && stamina > 0)
        {
            MAX_SPEED = 6.5f;
            stamina -= Time.deltaTime / 1.5f;
            staminaResetBuffer = 1.5f;
        }
        else
        {
            MAX_SPEED = 5f;
        }

        StaminaUpdate();

        ApplyFriction(FRICTION_COEF);
        Vector3 totalMovement = Vector3.zero;
        if(Input.GetKey(KeyCode.W))
        {
            totalMovement += new Vector3(0, MAX_SPEED, 0);
        }
        if(Input.GetKey(KeyCode.D))
        {
            totalMovement += new Vector3(MAX_SPEED, 0, 0);
        }
        if(Input.GetKey(KeyCode.A))
        {
            totalMovement += new Vector3(-MAX_SPEED, 0, 0);
        }
        if(Input.GetKey(KeyCode.S))
        {
            totalMovement += new Vector3(0, -MAX_SPEED, 0);
        }

        ApplyForce(Vector3.ClampMagnitude(totalMovement, MAX_SPEED));

        Movement();
    }

    public void StaminaUpdate()
    {
        if(staminaResetBuffer > 0)
        {
            staminaResetBuffer -= Time.deltaTime;
        }
        if(stamina < maxStamina && staminaResetBuffer <= 0)
        {
            stamina += Time.deltaTime;
        }

    }

    public void RotateVehicle()
    {
        transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(Input.mousePosition.x - position.x, Input.mousePosition.y - position.y));
    }
}
