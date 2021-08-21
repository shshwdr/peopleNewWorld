using Pool;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTurnView : TurnView
{
    public int normalFoodConsume = 2;
    public int rawFoodConsume = 3;
    public int poisonFoodDamage = 25;
    public int hungryDamage = 30;
    public int hungryHeal = 15;

    public GameObject mapButton;


    public GameObject normalBG;
    public GameObject finalBG;

    protected override void afterMoveCharacter()
    {

        if (CSDialogueManager.Instance.hasUnfinishedDialogue())
        {
            CSDialogueManager.Instance.showDialogue();
            EventPool.OptIn("dialogueFinished", afterMoveCharacterAndDialogue);
        }
        else
        {
            afterMoveCharacterAndDialogue();
        }

    }

    void afterMoveCharacterAndDialogue()
    {

        loadTutorials();
        uiPanel.SetActive(true);

        updateDescriptionText();

        updateAction();
    }

    void updateAction()
    {
        foreach(var chara in relatedCharacters)
        {
            if (chara.getStatus(CharacterStatus.sanity)<MainGameManager.Instance.forceRestSanity)
            {
                TutorialManager.Instance.showTutorialPanel(TutorialManager.tutorialAlert_SanityAlert);
                chara.GetComponent<ActionSelection>().selectAction((int)CharacterAction.rest);
            }


            if (chara.getStatus(CharacterStatus.health) < MainGameManager.Instance.forceRestHealth)
            {
                TutorialManager.Instance.showTutorialPanel(TutorialManager.tutorialAlert_HealthAlert);
                chara.GetComponent<ActionSelection>().selectAction((int)CharacterAction.rest);
            }
        }
    }
    public override void startTurnView()
    {
        base.startTurnView();
        CityManager.Instance. regenerateAllResources();
        foreach (var character in relatedCharacters)
        {
            character.updateAbilities();
            character.GetComponent<ActionSelection>().showAllSelectionUI();
            character.showStatus();
        }

        if (CityManager.Instance.currentCityInfo().isDestination)
        {
            normalBG.SetActive(false);
            finalBG.SetActive(true);
            MainGameManager.Instance.finishedGame = true;
            MusicManager.Instance.playNormal();
        }
        else
        {

            normalBG.SetActive(true);
            finalBG.SetActive(false);
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
                CharacterManager.Instance.characterList[i].increaseStatus(CharacterStatus.hungry, hungryHeal);
                continue;
            }
            else if (Inventory.Instance.getItemAmount(InventoryItem.rawFood) >= rawFoodConsume)
            {
                Inventory.Instance.consumeItem(InventoryItem.rawFood, rawFoodConsume);
                rawPeople.Add(CharacterManager.Instance.characterList[i].name);
                CharacterManager.Instance.characterList[i].decreaseStatus(CharacterStatus.sanity, poisonFoodDamage);
                CharacterManager.Instance.characterList[i].increaseStatus(CharacterStatus.hungry, hungryHeal);
                continue;
            }
            else
            {

                hungryPeople.Add(CharacterManager.Instance.characterList[i].name);
                CharacterManager.Instance.characterList[i].decreaseStatus(CharacterStatus.hungry, hungryDamage);

            }
        }

        if (Inventory.Instance.getItemAmount(InventoryItem.processedFood) + Inventory.Instance.getItemAmount(InventoryItem.rawFood) <8)
        {
            TutorialManager.Instance.showTutorialPanel(TutorialManager.tutorialAlert_NoInventoryAlert);
        }
        if (rawPeople.Count > 0)
        {
            foreach (var name in rawPeople)
            {
                res += name + " ";
            }
            res += "ate raw food and feel unhappy. ";
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
        MapController.Instance.closeMap();
        HUDManager.Instance.hideExplain();
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
