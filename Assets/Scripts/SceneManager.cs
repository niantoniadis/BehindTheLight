using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameStates { GameOver, Game }

public class SceneManager : MonoBehaviour
{
    GameStates gameState = GameStates.Game;
    public Player player;
    public Map sceneMap;

    // Start is called before the first frame update
    void Start()
    {
        gameState = GameStates.Game;
    }

    // Update is called once per frame
    void Update()
    {
        switch (gameState)
        {
            case GameStates.Game:
                player.Move();
                // sceneMap.LoadCurrentRoom();
                break;
        }
    }
}
