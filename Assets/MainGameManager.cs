using Pool;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainGameManager : Singleton<MainGameManager>
{
    public int startCharacterNum = 4;
    public bool[] unlockedAction;
    bool hasMainGameStarted = false;
    public bool[] unlockedItem = new bool[] { false, false };

    public bool finishedGame;
    public int forceRestSanity = 10;
    public int forceRestHealth = 10;
    void Start()
    {
    }

    public void startMainGame()
    {
        if (hasMainGameStarted)
        {
            return;
        }
        hasMainGameStarted = true;
        for (int i = 0; i < startCharacterNum; i++)
        {
            CharacterManager.Instance.addCharacter(i);
        }
        GameTurnManager.Instance.startGame();
        MusicManager.Instance.playNormal();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
