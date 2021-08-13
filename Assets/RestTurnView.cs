using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestTurnView : TurnView
{
    public int restHealHP = 15;
    protected override void updateDescriptionText()
    {
        base.updateDescriptionText();
        int collectAmount = relatedCharacters.Count;
        if (collectAmount > 1)
        {
            descriptionText.text = relatedCharacters[0].name + "and others are resting.";
        }
        else
        {
            descriptionText.text = relatedCharacters[0].name + "is resting.";
        }

        foreach(var ch in relatedCharacters){
            ch.heal(restHealHP);
        }
    }
}
