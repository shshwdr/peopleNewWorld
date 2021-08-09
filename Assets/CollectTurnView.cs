using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectTurnView : TurnView
{

    protected override void updateDescriptionText()
    {
        base.updateDescriptionText();
        int collectAmount = relatedCharacters.Count;
        descriptionText.text = "Collected " + collectAmount + " food.";
        Inventory.Instance.addItem(InventoryItem.processedFood, collectAmount);
    }
}
