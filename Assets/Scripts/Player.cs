using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Vector3 direction;
    Vector3 position;
    Vector3 velocity;
    Vector3 desiredVelocity;
    Vector3 acceleration;
    float angleOfRotation;
    float mass;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }



    public void RotateVehicle()
    {
        transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(Input.mousePosition.x - position.x, Input.mousePosition.y - position.y));
    }

    public void ApplyForce(Vector3 force)
    {
        acceleration += force / mass;
    }
}
