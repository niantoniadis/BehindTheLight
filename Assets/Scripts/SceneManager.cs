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
    public UIManager uIManager;
    public Material lineMat;
    public Room basic;
    List<Room> room;
    
    // Start is called before the first frame update
    void Start()
    {
        gameState = GameStates.Game;
        player = Instantiate(player.gameObject, Vector3.zero, Quaternion.identity).GetComponent<Player>();
        sceneMap = Instantiate(sceneMap.gameObject, Vector3.zero, Quaternion.identity).GetComponent<Map>();
        room = new List<Room>();
        room.Add(basic);
    }

    // Update is called once per frame
    void Update()
    {
        List<Room> rooms = room;
        switch (gameState)
        {
            case GameStates.Game:
                switch (player.CurrentState)
                {
                    case PlayerStates.Default:
                        player.Move();
                        if(Input.GetMouseButtonDown(0))
                        {
                            player.attacking = true;   
                        }
                        player.AnimateAttack();
                        if (player.attacking)
                        {
                            enemyManager.SwordCollisions(player, rooms[0]);
                        }
                        player.RotateVehicle();
                        break;
                }
                // sceneMap.LoadCurrentRoom();
                enemyManager.UpdateEnemyList(rooms);
                enemyManager.MoveEnemy(player, rooms);
                enemyManager.EnemyCollisions(player, rooms[0]);
                enemyManager.RemoveEnemies(rooms);
                uIManager.UpdatePlayerData(player);
                break;
            case GameStates.GameOver:
                Debug.Log("you lost bro");
                break;
        }
        gameState = player.IsDead() ? GameStates.GameOver : GameStates.Game;
    }
}
