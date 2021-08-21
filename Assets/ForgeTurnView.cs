using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForgeTurnView : TurnView
{
    public int baseForgeValue = 8;
    public int baseForgeOutput = 2;
    public float maxAffectRate = 0.05f;
    public CharacterAbility affectAbility = CharacterAbility.Dex;
    protected override void updateDescriptionText()
    {
        base.updateDescriptionText();
        int collectAmount = 0;
        int consumeAmount = 0;
        bool notEnoughRawFood = false;
        foreach (var chara in relatedCharacters)//sort with agile?
        {
            int maxConsumeFood = 0;
            maxConsumeFood += baseForgeValue;
            maxConsumeFood -= Mathf.FloorToInt((chara.getAbility(affectAbility) + 10) * maxAffectRate);
            int consumeFood = Mathf.Min(maxConsumeFood, Inventory.Instance.getItemAmount(InventoryItem.materials));
            if (consumeFood < maxConsumeFood)
            {
                notEnoughRawFood = true;
                continue;
            }
            Inventory.Instance.consumeItem(InventoryItem.materials, consumeFood);
            int currentCollect = baseForgeOutput;
            currentCollect += Mathf.FloorToInt(chara.getAbility(affectAbility) * maxAffectRate);
            showItemsCollected(chara, currentCollect);
            collectAmount += currentCollect;
            consumeAmount += consumeFood;
        }
        //relatedCharacters.Count * baseCollectValue;
        //var collects = collectItems(collectAmount);
        descriptionText.text = "Forged " + consumeAmount.ToString() + " " + Inventory.Instance.inventoryNameMap[InventoryItem.materials] + " to " +
            collectAmount.ToString() + " " + Inventory.Instance.inventoryNameMap[InventoryItem.weapon] + ". ";
        if (notEnoughRawFood)
        {
            descriptionText.text += "You can forge more but there is no " + Inventory.Instance.inventoryNameMap[InventoryItem.materials] + " left.";
        }
        Inventory.Instance.addItem(InventoryItem.weapon, collectAmount);

        foreach (var chara in relatedCharacters)
        {
            chara.increaseAbility(affectAbility, Random.Range(2, 5));
        }
        // CityManager.Instance.collectItemsFromCurrentCity(collects);
    }

    protected override void loadTutorials()
    {

        CSDialogueManager.Instance.addDialogue(7);
        TutorialManager.Instance.showTutorialPanel(TutorialManager.tutorialTurnIntro_Forge);
    }

    void showItemsCollected(Character chara, int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            chara.addTempItem(2);
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
