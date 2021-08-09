using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTurnView : TurnView
{

    public override void startTurnView()
    {
        base.startTurnView();
        foreach(var character in relatedCharacters)
        {
            character.GetComponent<ActionSelection>().showAllSelectionUI();
        }
    }
    public override bool shouldPlayTurn()
    {
        return true;
    }


    public override void stopTurnView()
    {
        base.stopTurnView(); 
        foreach (var character in relatedCharacters)
        {
            character.GetComponent<ActionSelection>().hideAllSelectionUI();
        }
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
