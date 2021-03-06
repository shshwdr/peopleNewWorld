using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheatManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //unlock all actions
        if (Input.GetKeyDown(KeyCode.P))
        {
            for(int i = 0;i< MainGameManager.Instance.unlockedAction.Length; i++)
            {
                MainGameManager.Instance.unlockedAction[i] = true;
            }
        }
        //unlock all map
        if (Input.GetKeyDown(KeyCode.O))
        {
            for (int i = 0; i < MainGameManager.Instance.unlockedAction.Length; i++)
            {
                CityManager.Instance.unlockWholeMap();
            }
        }
        //pass turn so wont get stuck by action not show up etc.
        if (Input.GetKeyDown(KeyCode.I))
        {
            PixelCrushers.DialogueSystem.DialogueManager.StopConversation();
            GameTurnManager.Instance.nextTurn();
        }
    }
}
