using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTurnView : TurnView
{
    public int normalFoodConsume = 2;
    public int rawFoodConsume = 3;
    public int poisonFoodDamage = 10;
    public int hungryDamage = 20;

    public GameObject mapButton;
    public override void startTurnView()
    {
        base.startTurnView();
        foreach(var character in relatedCharacters)
        {
            character.updateAbilities();
            character.GetComponent<ActionSelection>().showAllSelectionUI();
            character.showStatus();
        }

        if (MainGameManager.Instance.unlockedAction[MainGameManager.Instance.unlockedAction.Length - 1])
        {
            mapButton.SetActive(true);
        }
        else
        {

            mapButton.SetActive(false);
        }

        //showTutorial();

    }

    protected override void loadTutorials()
    {
        if (MainGameManager.Instance.unlockedAction[(int)CharacterAction.collect])
        {
            TutorialManager.Instance.showTutorialPanel(TutorialManager.tutorialActionIntro_Collect);
        }
        if (MainGameManager.Instance.unlockedAction[(int)CharacterAction.hunt])
        {
            TutorialManager.Instance.showTutorialPanel(TutorialManager.tutorialActionIntro_Hunt);
        }
        if (MainGameManager.Instance.unlockedAction[(int)CharacterAction.rest])
        {
            TutorialManager.Instance.showTutorialPanel(TutorialManager.tutorialActionIntro_Rest);
        }
        if (MainGameManager.Instance.unlockedAction[(int)CharacterAction.scout])
        {
            TutorialManager.Instance.showTutorialPanel(TutorialManager.tutorialActionIntro_Scout);
        }
        if (MainGameManager.Instance.unlockedAction[MainGameManager.Instance.unlockedAction.Length-1])
        {
            TutorialManager.Instance.showTutorialPanel(TutorialManager.tutorialActionIntro_Move);
        }
    }
    protected override void updateDescriptionText()
    {
        base.updateDescriptionText();
        if (GameTurnManager.Instance.currentTurnNum > 0)
        {

            descriptionText.text = consumeFood();
        }
    }
    string consumeFood()
    {
        string res = "";
        List<string> rawPeople = new List<string>();
        List<string> hungryPeople = new List<string>();
        for(int i =0;i< CharacterManager.Instance.characterList.Count; i++)
        {
            //consume 1 processed food
            if (Inventory.Instance.getItemAmount(InventoryItem.processedFood) >= normalFoodConsume)
            {
                Inventory.Instance.consumeItem(InventoryItem.processedFood, normalFoodConsume);
                continue;
            }
            else if (Inventory.Instance.getItemAmount(InventoryItem.rawFood) >= rawFoodConsume)
            {
                Inventory.Instance.consumeItem(InventoryItem.rawFood, rawFoodConsume);
                rawPeople.Add(relatedCharacters[i].name);
                relatedCharacters[i].decreaseStatus(CharacterStatus.sanity, poisonFoodDamage);
                continue;
            }
            //}else if (Inventory.Instance.getItemAmount(InventoryItem.poisonedFood) >= rawFoodConsume)
            //{
            //    Inventory.Instance.consumeItem(InventoryItem.poisonedFood, rawFoodConsume);
            //    poisonedPeople.Add(relatedCharacters[i].name);
            //    relatedCharacters[i].doDamage(poisonFoodDamage);
            //    continue;
            //}
            else
            {

                hungryPeople.Add(relatedCharacters[i].name);
                relatedCharacters[i].decreaseStatus(CharacterStatus.hungry, hungryDamage);
                //relatedCharacters[i].doDamage(hungryDamage);
            }
        }
        if (rawPeople.Count > 0)
        {
            foreach (var name in rawPeople)
            {
                res += name + " ";
            }
            res += "ate saw food and feel unhappy. ";
        }
        if (hungryPeople.Count > 0)
        {

            foreach (var name in hungryPeople)
            {
                res += name + " ";
            }
            res += "had nothing to eat and get hungry. ";
        }
        if(hungryPeople.Count == 0 && rawPeople.Count == 0)
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
