using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    float angleOfRotation;
    float lifeSpan;
    float cooldown;
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
                lifeSpan = 10f;
                cooldown = 0;
                break;

            case "LargeFlashPower":
                type = PowerUpType.LargeFlash;
                lifeSpan = 6f;
                cooldown = 4f;
                break;

            case "BackFlashPower":
                type = PowerUpType.BackFlash;
                lifeSpan = 3f;
                cooldown = 6f;
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
        lifeSpan -= Time.deltaTime;
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

    public float Cooldown
    {
        get
        {
            return cooldown;
        }
    }

    public float LifeSpan
    {
        get
        {
            return lifeSpan;
        }
    }
}
