using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    float angleOfRotation;
    CircleCollider2D collider;
    PowerUpType type;
    // Start is called before the first frame update
    void Start()
    {
        collider = GetComponent<CircleCollider2D>();
        switch (gameObject.tag)
        {
            case "HealthPower":
                type = PowerUpType.Health;
                break;

            case "LargeFlashPower":
                type = PowerUpType.LargeFlash;
                break;

            case "BackFlashPower":
                type = PowerUpType.BackFlash;
                break;

            case "TripleFlashPower":
                type = PowerUpType.TripleFlash;
                break;

            case "DoubleDamagePower":
                type = PowerUpType.DoubleDamage;
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        angleOfRotation += 2f;
        transform.rotation = Quaternion.Euler(0, 0, angleOfRotation);
    }

    public PowerUpType Type
    {
        get
        {
            return type;
        }
    } 
}
