using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameTurn { player, collect}
public class GameTurnManager : Singleton<GameTurnManager>
{
    public GameTurn currentTurn;
    public PlayerTurnView playerTurnView;
    public CollectTurnView collectTurnView;
    //public void startPlayerTurn()
    //{
    //    currentTurn = GameTurn.player;
    //    playerTurnView.startTurnView();

    //}

    public void nextTurn()
    {
        switch (currentTurn)
        {
            case GameTurn.player:
                playerTurnView.stopTurnView();
                currentTurn = GameTurn.collect;
                collectTurnView.startTurnView();
                break;
            case GameTurn.collect:
                collectTurnView.stopTurnView();
                currentTurn = GameTurn.player;
                playerTurnView.startTurnView();
                break;
        }
    }

    public void startGame()
    {
        currentTurn = GameTurn.player;
        playerTurnView.startTurnView();
    }
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
