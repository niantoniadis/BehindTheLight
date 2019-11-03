using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PowerUpType { Health, BackFlash, LargeFlash, DoubleDamage, TripleFlash }

public class PowerUpManager : MonoBehaviour
{
    public PowerUp health;
    public PowerUp behindFlash;
    public PowerUp largeFlash;
    public PowerUp tripleFlash;
    public PowerUp doubleDamage;
    List<PowerUp> powerUps;

    // Start is called before the first frame update
    void Start()
    {
        powerUps = new List<PowerUp>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChancePowerSpawn(Vector3 pos)
    {
        float rng = Random.Range(0f, 1f);

        if(rng >= 0.9 && pos.x < 30000f)
        {
            rng = Random.Range(0f, 1f);
            if(rng > 0.8)
            {
                powerUps.Add(Instantiate(health.gameObject, pos, Quaternion.identity).GetComponent<PowerUp>());
            }
            else if (rng > 0.6)
            {
                powerUps.Add(Instantiate(largeFlash.gameObject, pos, Quaternion.identity).GetComponent<PowerUp>());
            }
            else if (rng > 0.4)
            {
                powerUps.Add(Instantiate(largeFlash.gameObject, pos, Quaternion.identity).GetComponent<PowerUp>());
            }
            else if (rng > 0.2)
            {
                powerUps.Add(Instantiate(largeFlash.gameObject, pos, Quaternion.identity).GetComponent<PowerUp>());
            }
            else
            {
                powerUps.Add(Instantiate(health.gameObject, pos, Quaternion.identity).GetComponent<PowerUp>());
            }
        }
    }

    public void HandleCollision(Player player)
    {
        for (int i = 0; i < powerUps.Count; i++)
        {
            if (player.IsCollidingWith(powerUps[i].GetComponents<CircleCollider2D>()))
            {
                switch (powerUps[i].Type)
                {
                    case PowerUpType.Health:
                        player.Heal(5);
                        break;
                    case PowerUpType.LargeFlash:
                        player.EnlargeFlashlight(powerUps[i].Cooldown);
                        break;
                    case PowerUpType.DoubleDamage:
                        break;
                    case PowerUpType.TripleFlash:
                        break;
                    case PowerUpType.BackFlash:
                        break;
                }

                Destroy(powerUps[i].gameObject);
                powerUps.RemoveAt(i);
                i--;
            }
        }
    }

    public void DeleteUneeded()
    {
        for (int i = 0; i < powerUps.Count; i++)
        {
            if(powerUps[i].LifeSpan < 0)
            {
                Destroy(powerUps[i].gameObject);
                powerUps.RemoveAt(i);
                i--;
            }
        }
    }
}
