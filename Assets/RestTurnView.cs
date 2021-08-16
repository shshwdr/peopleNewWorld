using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestTurnView : TurnView
{

    static public string restReward_Cook = "While rest you found a recipe and it teaches you how to cook!";
    bool[] hasGotReward = new bool[1] ;
    public int restHealHP = 15;

    public override void startTurnView()
    {
        base.startTurnView();

        TutorialManager.Instance.unlockAction((int)CharacterAction.scout);

        if (!hasGotReward[0])
        {

            TutorialManager.Instance.unlockAction((int)CharacterAction.cook);
            TutorialManager.Instance.showTutorialPanel(restReward_Cook);
        }

    }
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
