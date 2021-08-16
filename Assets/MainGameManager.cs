using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainGameManager : Singleton<MainGameManager>
{
    public int startCharacterNum = 4;
    public bool[] unlockedAction;

    void Start()
    {
        for(int i = 0; i < startCharacterNum; i++)
        {
            CharacterManager.Instance.addCharacter(i);
        }
        GameTurnManager.Instance.startGame();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
