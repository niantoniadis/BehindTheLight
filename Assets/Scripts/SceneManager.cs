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
    public Room room;
    public List<Room> rooms = new List<Room>();
    public Material lineMat;
    
    // Start is called before the first frame update
    void Start()
    {
        gameState = GameStates.Game;
        player = Instantiate(player.gameObject, Vector3.zero, Quaternion.identity).GetComponent<Player>();
        rooms.Add(Instantiate(room.gameObject, Vector3.zero, Quaternion.identity).GetComponent<Room>());        
    }

    // Update is called once per frame
    void Update()
    {
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
                        if(player.attacking)
                        {
                            enemyManager.SwordCollisions(player, rooms[0]);
                        }
                        player.AnimateAttack();
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

    private void OnRenderObject()
    {
        lineMat.SetPass(0);
        GL.Begin(GL.LINES);
        GL.Vertex(player.Position);
        GL.Vertex(player.Position + player.Direction);
        GL.End();
    }
}
