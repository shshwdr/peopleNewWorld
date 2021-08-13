using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameTurn { player, collect, hunt, scout, rest,move}
public class GameTurnManager : Singleton<GameTurnManager>
{
    public int currentTurnNum = 0;
    public GameTurn currentTurn;

    public List<TurnView> turnViews;
    Dictionary<GameTurn, TurnView> turnViewKeyToItem;

    //public void startPlayerTurn()
    //{
    //    currentTurn = GameTurn.player;
    //    playerTurnView.startTurnView();

    //}

    public void move()
    {

        var currentTurnView = turnViewKeyToItem[currentTurn];
        currentTurnView.stopTurnView();

        int nextTurn = System.Enum.GetValues(typeof(GameTurn)).Length - 1;

        currentTurn = (GameTurn)nextTurn;
        var nextTurnView = turnViewKeyToItem[currentTurn];
        nextTurnView.startTurnView();
        currentTurnNum++;
    }
    public void nextTurn()
    {
        var currentTurnView = turnViewKeyToItem[currentTurn];
        currentTurnView.stopTurnView();
        while (true)
        {

            int nextTurn = (int)currentTurn + 1;
            if (nextTurn >= System.Enum.GetValues(typeof(GameTurn)).Length-1)
            {
                nextTurn = 0;
                currentTurnNum++;
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
