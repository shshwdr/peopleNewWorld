using Pool;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CSDialogueManager : Singleton<CSDialogueManager>
{
    List<int> unFinishedDialog = new List<int>();
    Dictionary<int, bool> finishedDialog = new Dictionary<int, bool>();

    public GameObject hud;
    public GameObject playerHud;
    public void startDialogue()
    {

        hud.SetActive(false);
        playerHud.SetActive(false);

        foreach(var chara in CharacterManager.Instance.characterList)
        {
            chara.GetComponent<ActionSelection>().hideAllSelectionUI();
        }
    }


    public void finishDialogue()
    {
        hud.SetActive(true);
        playerHud.SetActive(true);

        EventPool.Trigger("dialogueFinished");

        foreach (var chara in CharacterManager.Instance.characterList)
        {
            chara.GetComponent<ActionSelection>().showAllSelectionUI();
        }
    }
    public bool hasUnfinishedDialogue()
    {
        return unFinishedDialog.Count > 0;
    }
    public void addDialogue(int i)
    {
        if (finishedDialog.ContainsKey(i))
        {
            return;
        }
        unFinishedDialog.Add(i);
    }
    public void showDialogue()
    {
        PixelCrushers.DialogueSystem.DialogueManager.StopConversation();
        if (unFinishedDialog.Count > 0)
        {
            switch (unFinishedDialog[0])
            {
                case 0:

                    PixelCrushers.DialogueSystem.DialogueManager.StartConversation("after collection");
                    break;
                case 1:

                    PixelCrushers.DialogueSystem.DialogueManager.StartConversation("after hunt");
                    break;
                case 2:

                    PixelCrushers.DialogueSystem.DialogueManager.StartConversation("after rest");
                    break;
                case 3:

                    PixelCrushers.DialogueSystem.DialogueManager.StartConversation("after scout 1");
                    break;
                case 4:

                    PixelCrushers.DialogueSystem.DialogueManager.StartConversation("scout destination");
                    break;
                case 5:

                    PixelCrushers.DialogueSystem.DialogueManager.StartConversation("ending");
                    break;
                case 6:

                    PixelCrushers.DialogueSystem.DialogueManager.StartConversation("cook");
                    break;
                case 7:

                    PixelCrushers.DialogueSystem.DialogueManager.StartConversation("forge");
                    break;
                case 8:

                    PixelCrushers.DialogueSystem.DialogueManager.StartConversation("after arrive first place");
                    break;
                case 9:

                    PixelCrushers.DialogueSystem.DialogueManager.StartConversation("meet water");
                    break;
                case 10:

                    PixelCrushers.DialogueSystem.DialogueManager.StartConversation("horse");
                    break;
                case 11:

                    PixelCrushers.DialogueSystem.DialogueManager.StartConversation("boat");
                    break;
                case 12:

                    PixelCrushers.DialogueSystem.DialogueManager.StartConversation("center city");
                    break;
            }
            finishedDialog[unFinishedDialog[0]] = true;
            unFinishedDialog.RemoveAt(0);
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
