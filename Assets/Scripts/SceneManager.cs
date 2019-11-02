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
    public Material lineMat;
    
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
                switch (player.CurrentState)
                {
                    case PlayerStates.Default:
                        player.Move();
                        player.RotateVehicle();
                        break;
                }
                // sceneMap.LoadCurrentRoom();
                enemyManager.UpdateEnemyList();
                enemyManager.MoveEnemy(player);
                enemyManager.RemoveEnemies();
                uIManager.UpdatePlayerData(player);
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
