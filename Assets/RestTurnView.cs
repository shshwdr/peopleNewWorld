using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestTurnView : TurnView
{
    protected override void updateDescriptionText()
    {
        base.updateDescriptionText();
        int collectAmount = relatedCharacters.Count;
        if (collectAmount > 0)
        {
            
            descriptionText.text = relatedCharacters[0].name + " are resting.";
        }
    }
}
