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

        if (GameTurnManager.Instance.currentTurnNum > 0)
        {

        }

    }
    protected override void updateDescriptionText()
    {
        base.updateDescriptionText();
        descriptionText.text = consumeFood();
    }
    string consumeFood()
    {
        string res = "";
        List<string> poisonedPeople = new List<string>();
        List<string> hungryPeople = new List<string>();
        for(int i =0;i< relatedCharacters.Count; i++)
        {
            //consume 1 processed food
            if (Inventory.Instance.getItemAmount(InventoryItem.processedFood)>=2)
            {
                Inventory.Instance.consumeItem(InventoryItem.processedFood, 2);
                continue;
            }else if (Inventory.Instance.getItemAmount(InventoryItem.rawFood) >=3)
            {
                Inventory.Instance.consumeItem(InventoryItem.rawFood, 3);
                continue;
            }else if (Inventory.Instance.getItemAmount(InventoryItem.poisonedFood) >= 3)
            {
                Inventory.Instance.consumeItem(InventoryItem.poisonedFood, 3);
                poisonedPeople.Add(relatedCharacters[i].name);
                continue;
            }
            else
            {

                hungryPeople.Add(relatedCharacters[i].name);
            }
        }
        if (poisonedPeople.Count > 0)
        {
            foreach (var name in poisonedPeople)
            {
                res += name + " ";
            }
            res += "ate poisoned food and get unhealthy. ";
        }
        if (hungryPeople.Count > 0)
        {

            foreach (var name in hungryPeople)
            {
                res += name + " ";
            }
            res += "had nothing to eat and get unhealthy. ";
        }
        if(hungryPeople.Count == 0 && poisonedPeople.Count == 0)
        {
            res += "Everyone enjoyed their food and get happy.";
        }
        return res;
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
