using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameStates { GameOver, Game, Paused }

public class SceneManager : MonoBehaviour
{
    GameStates gameState = GameStates.Game;
    public Player player;

    public Map sceneMap;
    public EnemyManager enemyManager;
    public PowerUpManager powerUpManager;
    public UIManager uIManager;
    public GameObject pauseMenu;
    public Material lineMat;
    
    // Start is called before the first frame update
    void Start()
    {
        gameState = GameStates.Game;
        pauseMenu.SetActive(false);
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
                if(Input.GetKeyDown(KeyCode.P))
                {
                    gameState = GameStates.Paused;
                    break;
                }

                switch (player.CurrentState)
                {
                    case PlayerStates.Default:
                        player.Move();
                        if(Input.GetMouseButton(0))
                        {
                            player.Attacking = true;   
                        }
                        player.AnimateAttack();
                        if(player.Attacking)
                        {
                            enemyManager.SwordCollisions(player, currentRoom);
                        }
                        currentRoom.Walls(player);
                        player.RotateVehicle();
                        player.StaminaUpdate();
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
                Vector3 pos = enemyManager.RemoveEnemies(rooms);

                powerUpManager.ChancePowerSpawn(pos);
                powerUpManager.HandleCollision(player);

                sceneMap.UpdateMap(player);

                if (!pos.Equals(Vector3.positiveInfinity))
                {
                    player.Score += 100;
                }
                player.Score += (Time.deltaTime * 5);
                uIManager.UpdatePlayerData(player);
                break;
            case GameStates.GameOver:
                Debug.Log("you lost bro");
                break;
            case GameStates.Paused:
                pauseMenu.SetActive(true);
                if(Input.GetKeyDown(KeyCode.P))
                {
                    pauseMenu.SetActive(false);
                    gameState = GameStates.Game;
                }
                break;
        }
        if(gameState != GameStates.Paused)
            gameState = player.IsDead() ? GameStates.GameOver : GameStates.Game;
    }
}
