using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Vehicle
{

    // Start is called before the first frame update
    void Start()
    {
        mass = 10;
        maxSpeed = 5f;
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
    }

    public void Move()
    {
        Vector3 totalMovement = Vector3.zero;
        if(Input.GetKey(KeyCode.W))
        {
            totalMovement += new Vector3(0, maxSpeed, 0);
        }
        if(Input.GetKey(KeyCode.D))
        {
            totalMovement += new Vector3(maxSpeed, 0, 0);
        }
        if(Input.GetKey(KeyCode.A))
        {
            totalMovement += new Vector3(-maxSpeed, 0, 0);
        }
        if(Input.GetKey(KeyCode.S))
        {
            totalMovement += new Vector3(0, -maxSpeed, 0);
        }

        SetVelocity(Vector3.ClampMagnitude(totalMovement, maxSpeed));
    }

    public void RotateVehicle()
    {
        transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(Input.mousePosition.x - position.x, Input.mousePosition.y - position.y));
    }
}
