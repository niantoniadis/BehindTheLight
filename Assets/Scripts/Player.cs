﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerStates { Default, Attacking }

public class Player : Vehicle
{
    public GameObject sword;
    public CircleCollider2D lightCollider;
    public CircleCollider2D attack;
    public Light frontLight;
    float attackingTimer = 0;
    float attackTime;
    PlayerStates currentState;
    Vector2 coords;
    float maxStamina;
    float staminaResetBuffer;
    float stamina;
    float hitBuffer;
    bool invincible;
    bool attacking = false;
    float score = 0;
    bool flashEnlarged;
    float flashEnlargedTimer;
    GameObject backLight;
    float backFlashTimer;

    // Start is called before the first frame update
    void Start()
    {
        currentState = PlayerStates.Default;
        direction = new Vector3(1, 0, 0);
        velocity = Vector3.zero;
        acceleration = Vector3.zero;
        position = transform.position;

        ACCELERATION_SCALE = 30.0f;
        MAX_SPEED = 5f;
        FRICTION_COEF = 1.0f;
        health = 20;
        maxHealth = 20;
        mass = 1;
        maxStamina = 4f;
        stamina = maxStamina;
        staminaResetBuffer = 1.5f;
        attackTime = 0.1f;
        knockback = 24;
        damage = 3;
        flashEnlarged = false;
        flashEnlargedTimer = 0f;
        backFlashTimer = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (flashEnlarged)
        {
            flashEnlargedTimer -= Time.deltaTime;
        }
        if(flashEnlargedTimer <= 0 && flashEnlarged)
        {
            ResetFlashlight();
        }
        if(backFlashTimer > 0)
        {
            backFlashTimer -= Time.deltaTime;
        }
        if(backFlashTimer <= 0)
        {
            RemoveBackFlashlight();
        }
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

    public PlayerStates CurrentState
    {
        get
        {
            return currentState;
        }
    }

    public bool Attacking
    {
        get
        {
            return attacking;
        }
        set 
        {
            attacking = value;
        }
    }
    public float Score
    {
        get
        {
            return score;
        }
        set
        {
            score = value;
        }
    }

    public override void Movement()
    {
        velocity += acceleration * Time.deltaTime * ACCELERATION_SCALE;

        if (acceleration.magnitude == 0)
        {
            velocity *= 0.7f;
        }

        velocity = Vector3.ClampMagnitude(velocity, MAX_SPEED);
        position += velocity * Time.deltaTime;

        transform.rotation = Quaternion.Euler(0, 0, rotation);
        transform.position = position;

        acceleration = Vector3.zero;
        
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
        if(velocity.magnitude > 0)
        {
            ApplyFriction(FRICTION_COEF);
        }

        ApplyForce(Vector3.ClampMagnitude(totalMovement, MAX_SPEED));

        Movement();

        RotateSword();
    }

    public void AnimateAttack()
    {
        if(attacking)
        {
            attackingTimer += Time.deltaTime;
            float swordRotation = Mathf.Atan2(-direction.x, direction.y) * Mathf.Rad2Deg;
            swordRotation += 130f;
            sword.transform.rotation = Quaternion.Euler(0, 0, swordRotation - 80 * attackingTimer/attackTime);
            if(attackingTimer >= attackTime)
            {
                attackingTimer = 0;
                attacking = false;
            }
        }
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
        float prevRotation = rotation;
        rotation = Mathf.Atan2(position.x - Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y - position.y) * Mathf.Rad2Deg + 90;
        direction = Quaternion.Euler(0, 0, rotation - prevRotation) * direction;
    }

    public void RotateSword()
    {
        if (velocity.magnitude > 0.5f)
        {
            coords = new Vector2(velocity.x, -velocity.y);
        }

        float swordRotation;
        swordRotation = Mathf.Atan2(coords.x, coords.y) * Mathf.Rad2Deg + 80;
        sword.transform.rotation = Quaternion.Euler(0, 0, swordRotation);
    }

    public void EnlargeFlashlight(float cooldown)
    {
        frontLight.spotAngle = 46.24f;
        flashEnlarged = true;
        flashEnlargedTimer = cooldown;
    }

    public void ResetFlashlight()
    {
        frontLight.spotAngle = 26.21703f;
        flashEnlarged = false;
        flashEnlargedTimer = 0;
    }
    
    public void SpawnBackFlashlight(float cooldown)
    {
        backFlashTimer = cooldown;

        if (backLight == null && backFlashTimer > 0)
        {
            Vector3 pos = frontLight.transform.position;
            backLight = Instantiate(frontLight.gameObject, new Vector3(-pos.x, pos.y, 0), Quaternion.identity);
            backLight.transform.parent = frontLight.transform.parent;
        }
    }

    public void RemoveBackFlashlight()
    {
        if (backLight != null && backFlashTimer <= 0)
        {
            Destroy(backLight);
        }
    }

    public void IncStamina(float count)
    {
        stamina += count;
    }
}
