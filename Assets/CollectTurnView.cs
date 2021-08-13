using Pool;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectTurnView : TurnView
{
    public int baseCollectValue = 3;
    protected override void updateDescriptionText()
    {
        base.updateDescriptionText();
        int collectAmount = relatedCharacters.Count * baseCollectValue;
        var collects = collectItems(collectAmount);
        descriptionText.text = collectString(collects);
        Inventory.Instance.addItems(collects);

        EventPool.Trigger("updateCityResource");
       // CityManager.Instance.collectItemsFromCurrentCity(collects);
    }

    string collectString(int[] collects)
    {
        string res = "Collected ";
        bool collectedSomething = false;
        string inventoryString = Inventory.Instance. inventoryItemsToString(collects);
        res += inventoryString;
        if ((inventoryString.Length==0))
        {
            res += "Nothing...";
        }
        else
        {

            res += "!";
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
