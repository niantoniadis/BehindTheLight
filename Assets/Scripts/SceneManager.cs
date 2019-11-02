using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameStates { GameOver, Game }

public class SceneManager : MonoBehaviour
{
    GameStates gameState = GameStates.Game;
    public Player player;

    public Map sceneMap;
    public EnemyManager enemyManager;
    public Room room;

    // Start is called before the first frame update
    void Start()
    {
        gameState = GameStates.Game;
        player = Instantiate(player.gameObject, Vector3.zero, Quaternion.identity).GetComponent<Player>();
        room = Instantiate(room.gameObject, Vector3.zero, Quaternion.identity).GetComponent<Room>();
        enemyManager.AddRoom(room);
    }

    // Update is called once per frame
    void Update()
    {
        switch (gameState)
        {
            case GameStates.Game:
                player.Move();
                // sceneMap.LoadCurrentRoom();
                enemyManager.UpdateEnemyList();
                enemyManager.MoveEnemy(player);
                enemyManager.RemoveEnemies();
                break;
        }
        gameState = player.IsDead() ? GameStates.GameOver : GameStates.Game;
    }
}
