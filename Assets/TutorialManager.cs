using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : Singleton<TutorialManager>
{
    public bool skipTutorial;

    static public string tutorialActionIntro_Collect = "Each day you can select an action for each character to do.";
    static public string tutorialActionIntro_Collect2 = "For now let's just <color=red>Collect</color> resources for everybody and get familiar with this area!";

    static public string tutorialActionIntro_Hunt = "You can select <color=red>Hunt</color> to fight some animals and get more resouces. ";
    static public string tutorialActionIntro_Hunt2 = "Click on some characters and select Hunt. \nRemember many hands make light work!";

    static public string tutorialActionIntro_Rest = "If any of the character get hurt, it's always a good idea to put him in <color=red>Rest</color>.";


    static public string tutorialActionIntro_Scout = "We might have explored enough for current place. It's time to scout for other places. You can only send one scout at a time since we can't afford someone destroy the rocket!";

    static public string tutorialActionIntro_Move = "Great you've found another place to stay. Move there when you think it's time. You can always move back later.";


    static public string tutorialTurnIntro_Collect = "You can collect food and items in this area, the amount each one can collect is affect by the <color=red>Strength</color>.";
    static public string tutorialTurnIntro_Collect2 = "Check the resources remains for each place on top! If there is no resources left, you can't collect anything";


    static public string tutorialTurnIntro_Cook = "You can cook food here, the amount each one can cook and the amount of cooked food is affect by the <color=red>Agility</color>.";
    static public string tutorialTurnIntro_Cook2 = "Check the raw food remins! If there is no raw food left, you can't cook anything";


    static public string tutorialTurnIntro_Forge = "You can forge weapon here, the material needed and the amount of weapon forged is affect by the <color=red>Dexterity</color>.";
    static public string tutorialTurnIntro_Forge2 = "Check the material remins! If there is not enough material left, you can't forge anything";


    static public string tutorialTurnIntro_Hunt = "While hunting, you need to first select a monster group to hunt, then arrange the position of characters to attack monsters.";
    static public string tutorialTurnIntro_Hunt2 = "The closer a character is to a monster, the more damage he will deal to the closest monster, but he will get more damage too.";
    static public string tutorialTurnIntro_Hunt3 = "The <color=red>Strength</color> affect how much damage a character can deal to a monster, <color=red>Agility</color> affect rate to avoid be attacked, <color=red>Dexterity</color> affect hit rate.";
    static public string tutorialTurnIntro_Hunt4 = "If you have weapons, they will be used automatically and deal more damage to monster";


    static public string tutorialTurnIntro_Rest = "You can rest here and restore <color=red>Health</color> and <color=red>Sanity</color>.";
    static public string tutorialTurnIntro_Rest2 = "Sometimes you can find something special here while resting. The chance to find stuff is decided by <color=red>Inteligence</color>.";

    static public string tutorialTurnIntro_Scout = "You can scout nearby places. Your target is to find another place for all of you to settle up.";
    static public string tutorialTurnIntro_Scout2 = "Click the directions to move each day. Scout would cost lots of sanity, don't force yourself!";



    static public string tutorialAlert_SanityAlert = "<color=red>Sanity</color> of some of you is too low! You can't do anything other than rest to heal when this happens!";
    static public string tutorialAlert_SanityAlertScout = "<color=red>Sanity</color> of scouter is too low! You are force to return when this happens!";
    static public string tutorialAlert_HealthAlert = "<color=red>Health</color> of some of you is too low! You can't do anything other than rest to heal when this happens!";
    static public string tutorialAlert_HungryAlert = "<color=red>Hunger</color> of some of you is too low! Your <color=red>Health</color> will keep decreasing when this happens!";

    List<string> tutorialStack = new List<string>();

    Dictionary<string, bool> hadTutorialShown = new Dictionary<string, bool>();

    Dictionary<string, string> finishDialogToStartDialog = new Dictionary<string, string>() {
        {tutorialActionIntro_Collect,tutorialActionIntro_Collect2 },
        {tutorialActionIntro_Hunt,tutorialActionIntro_Hunt2 },
        {tutorialTurnIntro_Collect,tutorialTurnIntro_Collect2 },
        {tutorialTurnIntro_Cook,tutorialTurnIntro_Cook2 },
        {tutorialTurnIntro_Forge,tutorialTurnIntro_Forge2 },
        {tutorialTurnIntro_Hunt,tutorialTurnIntro_Hunt2 },
        {tutorialTurnIntro_Hunt2,tutorialTurnIntro_Hunt3 },
        {tutorialTurnIntro_Hunt3,tutorialTurnIntro_Hunt4 },
        {tutorialTurnIntro_Rest,tutorialTurnIntro_Rest2 },
        {tutorialTurnIntro_Scout,tutorialTurnIntro_Scout2 },
    };

    public void finishPopup(string key)
    {
        if (skipTutorial) return;
        if (finishDialogToStartDialog.ContainsKey(key))
        {
            TutorialPanel.Instance.Init(finishDialogToStartDialog[key]);
        }
        else
        {
            if (tutorialStack.Count > 0)
            {
                showTutorialPanel(tutorialStack[0]);
                tutorialStack.RemoveAt(0);
            }
        }

    }

    public void showTutorialPanel(string text)
    {
        if (skipTutorial) return;
        if (!hadTutorialShown.ContainsKey(text))
        {

            if (TutorialPanel.Instance.isShowing)
            {
                tutorialStack.Add(text);
            }
            else
            {
                TutorialPanel.Instance.Init(text);
                hadTutorialShown[text] = true;
            }

        }

    }
    public void unlockAction(int i)
    {
        MainGameManager.Instance.unlockedAction[i] = true;
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
