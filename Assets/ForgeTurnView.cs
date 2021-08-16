using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForgeTurnView : TurnView
{
    public int baseForgeValue = 1;
    public int baseForgeCost = 4;
    protected override void updateDescriptionText()
    {
        base.updateDescriptionText();
        int cookAmount = relatedCharacters.Count * baseForgeValue;
        //var collects = collectItems(collectAmount);
        descriptionText.text = "forge something (WIP)";//collectString(collects);
        //Inventory.Instance.addItems(collects);

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
