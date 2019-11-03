using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject healthBar;
    public GameObject staminaBar;
    public GameObject score;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdatePlayerData(Player player)
    {
        healthBar.transform.localScale = new Vector3(player.Health / player.MaxHealth, healthBar.transform.localScale.y, 0);
        staminaBar.transform.localScale = new Vector3(player.Stamina / player.MaxStamina, staminaBar.transform.localScale.y, 0);
        score.GetComponent<TextMesh>().text = "Score: " + player.Score.ToString();
    }
}
