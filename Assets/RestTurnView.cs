using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestTurnView : TurnView
{
    int currentEventId = 0;
    int currentEventCount = 0;
    public int[] eventMaxTime = new int[] { 1, 2, 3 ,3,3};//cook, forge, move faster,boat, food

    static public string restReward_Cook = "While rest {0} found a device to ignite wood, you can <color=red>cook</color> now!";
    static public string restReward_Forge = "While rest {0} found a device to forge materials, you can <color=red>forge</color> weapon now!";
    static public string restReward_Bike = "While rest {0} tamed some horses, now <color=red>scout cost less sanity</color>!";
    static public string restReward_Boat = "While rest {0} figured out how to make a boat, you can <color=red>pass water</color> now!";
    static public string restReward_Food = "While rest {0} found some <color=red>supply</color> left in spaceship, what a surprise!";
    bool[] hasGotReward = new bool[1];
    public int restHealHP = 20;
    public int restHealSanity = 20;

    public GameObject fireCamp;
    public GameObject forge;
    public GameObject airTank;
    public GameObject boat;

    void triggerEvent()
    {
        switch (currentEventId)
        {
            case 0:
                TutorialManager.Instance.unlockAction((int)CharacterAction.cook);
                TutorialManager.Instance.showTutorialPanel(string.Format(restReward_Cook, relatedCharacters[0].name));
                fireCamp.SetActive(true);
                break;
            case 1:
                TutorialManager.Instance.unlockAction((int)CharacterAction.forge);
                TutorialManager.Instance.showTutorialPanel(string.Format(restReward_Forge, relatedCharacters[0].name));
                forge.SetActive(true);
                break;
            case 2:
                MainGameManager.Instance.unlockedItem[0] = true;

                CSDialogueManager.Instance.addDialogue(10);
                TutorialManager.Instance.showTutorialPanel(string.Format(restReward_Bike, relatedCharacters[0].name));
                airTank.SetActive(true);
                break;
            case 3:
                CSDialogueManager.Instance.addDialogue(11);
                MainGameManager.Instance.unlockedItem[1] = true;
                TutorialManager.Instance.showTutorialPanel(string.Format(restReward_Boat, relatedCharacters[0].name));
                boat.SetActive(true);
                break;
            case 4:
                TutorialManager.Instance.showTutorialPanel(string.Format(restReward_Food, relatedCharacters[0].name));
                Inventory.Instance.addItem(InventoryItem.processedFood, 8);
                break;
        }
    }

    public override void startTurnView()
    {
        base.startTurnView();



    }

    protected override void loadTutorials()
    {
        base.loadTutorials();
        CSDialogueManager.Instance.addDialogue(2);
        TutorialManager.Instance.unlockAction((int)CharacterAction.scout);
        TutorialManager.Instance.showTutorialPanel(TutorialManager.tutorialTurnIntro_Rest);
        //if (!hasGotReward[0])
        //{
        //    TutorialManager.Instance.unlockAction((int)CharacterAction.cook);
        //    TutorialManager.Instance.showTutorialPanel(restReward_Cook);
        //    fireCamp.SetActive(true);
        //}
    }
    protected override void updateDescriptionText()
    {
        base.updateDescriptionText();
        int collectAmount = relatedCharacters.Count;
        if (collectAmount > 1)
        {
            descriptionText.text = relatedCharacters[0].name + " and others are resting. They read some books and get smarter. ";
        }
        else
        {
            descriptionText.text = relatedCharacters[0].name + " is resting. He reads some books and gets smarter. ";
        }

        foreach (var ch in relatedCharacters)
        {
            ch.heal(restHealHP);
            ch.increaseStatus(CharacterStatus.sanity, restHealSanity);
            var affectAbility = CharacterAbility.Int;
            ch.increaseAbility(affectAbility, Random.Range(1, 4));
        }
    }

    protected override void afterMoveCharacter()
    {
        base.afterMoveCharacter();
        for (int i = 0; i < relatedCharacters.Count; i++)
        {
            relatedCharacters[i].showStatus(CharacterStatus.health);
            relatedCharacters[i].showStatus(CharacterStatus.sanity);
            currentEventCount++;
        }
        if (currentEventCount >= eventMaxTime[currentEventId])
        {
            currentEventCount = 0;
            triggerEvent();
            currentEventId++;
            if (currentEventId == eventMaxTime.Length)
            {
                currentEventId--;
            }
        }
    }
}
