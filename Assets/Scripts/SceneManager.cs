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
    
    // Start is called before the first frame update
    void Start()
    {
        gameState = GameStates.Game;
        player = Instantiate(player.gameObject, Vector3.zero, Quaternion.identity).GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        List<Room> rooms = sceneMap.GetRooms();
        Room currentRoom = rooms[0]; // change to sceneMap.CurrentRoom;
        switch (gameState)
        {
            case GameStates.Game:
                switch (player.CurrentState)
                {
                    case PlayerStates.Default:
                        player.Move();
                        if(Input.GetMouseButtonDown(0))
                        {
                            Debug.Log("dingdong");
                            player.attacking = true;   
                        }
                        player.AnimateAttack();
                        if (player.attacking)
                        {
                            enemyManager.SwordCollisions(player, currentRoom);
                        }
                        player.RotateVehicle();
                        break;
                }
                currentRoom.ActivateSpawners();
                foreach(Room r in rooms)
                {
                    if(r != currentRoom)
                    {
                        r.DeactivateSpawners();
                    }
                }
                enemyManager.UpdateEnemyList(rooms);
                enemyManager.MoveEnemy(player, rooms);
                enemyManager.EnemyCollisions(player, currentRoom);
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
