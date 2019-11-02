﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Vehicle
{

    // Start is called before the first frame update
    void Start()
    {
        ACCELERATION_SCALE = 30.0f;
        MAX_SPEED = 1.5f;
        FRICTION_COEF = 1.0f;

        mass = 1;
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
    }

    public void Move()
    {
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
    }

    public void RotateVehicle()
    {
        transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(Input.mousePosition.x - position.x, Input.mousePosition.y - position.y));
    }
}
