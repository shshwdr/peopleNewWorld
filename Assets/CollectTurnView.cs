using Pool;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectTurnView : TurnView
{
    public int baseCollectValue = 3;
    public float maxAffectRate = 0.1f;
    public CharacterAbility affectAbility = CharacterAbility.Str;

    public GameObject itemAmount;

    public override void startTurnView()
    {
        base.startTurnView();
    }

    protected override void loadTutorials()
    {
        CSDialogueManager.Instance.addDialogue(0);
        TutorialManager.Instance.unlockAction((int)CharacterAction.hunt);
        TutorialManager.Instance.showTutorialPanel(TutorialManager.tutorialTurnIntro_Collect);
    }

    protected override void updateDescriptionText()
    {
        base.updateDescriptionText();
        int collectAmount = 0;
        foreach(var chara in relatedCharacters)
        {
            int charCollect = 0;
            charCollect += baseCollectValue;
            charCollect += Mathf.FloorToInt(chara.getAbility(affectAbility) * maxAffectRate);
            collectAmount += charCollect;
            showItemsCollected(chara, charCollect);
        }
        //relatedCharacters.Count * baseCollectValue;
        var collects = collectItems(collectAmount);
        descriptionText.text = collectString(collects);
        Inventory.Instance.addItems(collects);

        EventPool.Trigger("updateCityResource");
       // CityManager.Instance.collectItemsFromCurrentCity(collects);
    }

    void showItemsCollected(Character chara, int amount)
    {
        for(int i = 0; i < amount; i++)
        {
            chara.addTempItem(0);
        }
    }

    string collectString(int[] collects)
    {
        string res = "Collected ";
        string inventoryString = Inventory.Instance. inventoryItemsToString(collects);
        res += inventoryString;
        if ((inventoryString.Length==0))
        {
            res += "Nothing... ";
        }
        else
        {

            res += "! ";
        }

        foreach (var chara in relatedCharacters)
        {
            chara.increaseAbility(affectAbility, Random.Range( 1,3));
        }
        if(relatedCharacters.Count > 1)
        {

            res += "They get stronger!";
        }
        else
        {

            res += "He get stronger!";
        }
        return res;
    }

    int totalResourceAmount(int[] collectables)
    {
        int res = 0;
        foreach(var val in collectables)
        {
            res += val;
        }
        return res;
    }

    int[] collectItems(int collectAmount)
    {
        int[] collectables = CityManager.Instance.currentCityInfo().collectable;
        int totalCollectableAmount = totalResourceAmount(collectables);
        int[] res = new int[collectables.Length];
        for(int i = 0; i < Mathf.Min(collectAmount,totalCollectableAmount); i++)
        {
            int loopCount = 0;
            while (true)
            {
                loopCount++;
                int rand = Random.Range(0, collectables.Length);
                if (collectables[rand] > 0)
                {
                    collectables[rand] -= 1;
                    res[rand] += 1;
                    break;
                }
                if (loopCount > 100)
                {
                    break;
                }
            }
        }
        return res;
    }

}
