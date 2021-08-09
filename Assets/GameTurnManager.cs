using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameTurn { player, collect,rest}
public class GameTurnManager : Singleton<GameTurnManager>
{
    public GameTurn currentTurn;

    public List<TurnView> turnViews;
    Dictionary<GameTurn, TurnView> turnViewKeyToItem;

    //public void startPlayerTurn()
    //{
    //    currentTurn = GameTurn.player;
    //    playerTurnView.startTurnView();

    //}

    public void nextTurn()
    {
        var currentTurnView = turnViewKeyToItem[currentTurn];
        currentTurnView.stopTurnView();
        while (true)
        {

            int nextTurn = (int)currentTurn + 1;
            if (nextTurn >= System.Enum.GetValues(typeof(GameTurn)).Length)
            {
                nextTurn = 0;
            }
            currentTurn = (GameTurn)nextTurn;

            var nextTurnView = turnViewKeyToItem[currentTurn];
            if (nextTurnView.shouldPlayTurn())
            {

                nextTurnView.startTurnView();
                break;
            }
        }

    }

    public void startGame()
    {
        currentTurn = GameTurn.player;

        turnViews[0].startTurnView();
    }
    
    // Start is called before the first frame update
    void Start()
    {
        turnViewKeyToItem = new Dictionary<GameTurn, TurnView>();
        foreach(var turnview in turnViews)
        {
            turnViewKeyToItem[turnview.gameTurn] = turnview;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
