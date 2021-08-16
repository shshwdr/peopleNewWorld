using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CookTurnView : TurnView
{

    public int baseCookValue = 3;
    public float maxAffectRate = 0.05f;
    public CharacterAbility affectAbility = CharacterAbility.Agi;
    protected override void updateDescriptionText()
    {
        base.updateDescriptionText();
        int collectAmount = 0;
        int consumeAmount = 0;
        bool notEnoughRawFood = false;
        foreach (var chara in relatedCharacters)//sort with agile?
        {
            int maxConsumeFood = 0;
            maxConsumeFood += baseCookValue;
            maxConsumeFood += Mathf.FloorToInt((chara.getAbility(affectAbility) + 10) * maxAffectRate);
            int consumeFood = Mathf.Min(maxConsumeFood, Inventory.Instance.getItemAmount(InventoryItem.rawFood));
            if(consumeFood< maxConsumeFood)
            {
                notEnoughRawFood = true;
            }
            Inventory.Instance.consumeItem(InventoryItem.rawFood, consumeFood);
            int currentCollect = consumeFood;
            currentCollect += Mathf.FloorToInt(chara.getAbility(affectAbility) * maxAffectRate);
            currentCollect = Mathf.CeilToInt((float)currentCollect * (float)consumeFood / (float)maxConsumeFood);
            showItemsCollected(chara, currentCollect);
            collectAmount += currentCollect;
            consumeAmount += consumeFood;
        }
        //relatedCharacters.Count * baseCollectValue;
        //var collects = collectItems(collectAmount);
        descriptionText.text = "Cooked " + consumeAmount.ToString() + " " + Inventory.Instance.inventoryNameMap[InventoryItem.rawFood] + " to " +
            collectAmount.ToString() + " " + Inventory.Instance.inventoryNameMap[InventoryItem.processedFood] + ". ";
        if (notEnoughRawFood)
        {
            descriptionText.text += "You can cook more but there is no " + Inventory.Instance.inventoryNameMap[InventoryItem.rawFood] + " left.";
        }
        Inventory.Instance.addItem(InventoryItem.processedFood, collectAmount);

        foreach (var chara in relatedCharacters)
        {
            chara.increaseAbility(affectAbility, 1);
        }
        // CityManager.Instance.collectItemsFromCurrentCity(collects);
    }

    protected override void loadTutorials()
    {

        TutorialManager.Instance.showTutorialPanel(TutorialManager.tutorialTurnIntro_Cook);
    }

    void showItemsCollected(Character chara, int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            chara.addTempItem(1);
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
