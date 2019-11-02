using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameStates { GameOver, Game }

public class SceneManager : MonoBehaviour
{
    GameStates gameState = GameStates.Game;
    public Player player;
    public EnemyManager enemyManager;
    public Room room;

    // Start is called before the first frame update
    void Start()
    {
        gameState = GameStates.Game;
        room = Instantiate(room.gameObject, Vector3.zero, Quaternion.identity).GetComponent<Room>();
    }

    // Update is called once per frame
    void Update()
    {
        switch (gameState)
        {
            case GameStates.Game:
                player.Move();
                enemyManager.MoveEnemy(player);
                break;
        }
    }
}
