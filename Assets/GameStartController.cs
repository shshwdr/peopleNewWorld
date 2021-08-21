using PixelCrushers.DialogueSystem;
using Pool;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStartController : Singleton<GameStartController>
{
    public bool hasStartStory = true;

    public GameObject spaceShip;
    public GameObject[] characters;

    public DialogueSystemTrigger trigger;
    // Start is called before the first frame update
    void Start()
    {
    }

    public void StartPlay()
    {
        if (hasStartStory)
        {

            MusicManager.Instance.playStart();
            spaceShip.GetComponent<Animator>().enabled = (true);
            SFXManager.Instance.playSound(SFXManager.Instance.landingClip);
            StartCoroutine(waitLandingFinished());
        }
        else
        {
            CSDialogueManager.Instance.finishDialogue();
            MainGameManager.Instance.startMainGame();
        }
    }

    IEnumerator waitLandingFinished()
    {
        yield return new WaitForSeconds(0.1f);
        float landTime = spaceShip.GetComponent<Animator>().GetCurrentAnimatorClipInfo(0).Length;
        yield return new WaitForSeconds(landTime);
        foreach (var ch in characters)
        {
            ch.transform.position = spaceShip.transform.position;
            ch.GetComponent<Character>().getIntoStory();
            ch.GetComponentInChildren<Animator>().gameObject.SetActive(false);
        }
        yield return new WaitForSeconds(0.3f);
        PixelCrushers.DialogueSystem.DialogueManager.StartConversation("start");
        EventPool.OptIn("dialogueFinished", finishStartDialogue);
    }


    void finishStartDialogue()
    {

        foreach (var ch in CharacterManager.Instance.characterList)
        {
            ch.GetComponent<Character>().leaveStory();
        }
        MainGameManager.Instance.startMainGame();
        //gameObject.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
