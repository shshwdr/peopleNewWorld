using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HUDManager : Singleton<HUDManager>
{

    public GameObject explainPanel;
    public TMP_Text explainLabel;
    float time;

    List<List<string>> explains = new List<List<string>>()
    {
        //explain for inventory
        new List<string>()
        {
            "How many <color=red>food</color> you have\nEach character need to each two food a day.",
            "How many <color=red>raw food</color> you have\nCharacter's sanity will drop if each raw food.",
            "How many <color=red>material</color> you have\nCan be used to make weapon.",
            "How many <color=red>weapon</color> you have\nCan be used in hunt.",
        },
        //explain for area resources
        new List<string>()
        {
            "How many <color=red>food</color> left in this area that you can collect\nit will regerate over time.",
            "How many <color=red>raw food</color> left in this area that you can collect\nit will regerate over time.",
            "How many <color=red>material</color> left in this area that you can collect\nit will regerate over time.",
            "How many <color=red>weapon</color> left in this area that you can collect\nit will regerate over time.",
        },
        //explain for area monster
        new List<string>()
        {
            "How many <color=red>deer</color> left in this area that you can hunt\nit will regerate over time.",
            "How many <color=red>unicorn</color> left in this area that you can hunt\nit will regerate over time.",
            "How many <color=red>swamp unicorn</color> left in this area that you can hunt\nit will regerate over time.",
            "How many <color=red>deamon deer</color> left in this area that you can hunt\nit will regerate over time.",
            "How many <color=red>empire buck</color> left in this area that you can hunt\nit will regerate over time.",
        },
        //explain for ability Int,Dex,Agi,Str
        new List<string>()
        {
            "<color=red>Duration</color>, sanity and hungry drops slower.\nIncrease in resting and hunting.",

            "<color=red>Dexterity</color>, forge more weapon and higher rate to hit.\nIncrease in forging and hunting.",
            "<color=red>Agility</color>, cook more food and higher rate to avoid.\nIncrease in cooking and hunting.",
            "<color=red>Strength</color>, collect more resources and higher damage.\nIncrease in collecting and hunting.",
        },
        // health, hungry, sanity 
        new List<string>()
        {
            "<color=red>Health</color>, decrease when get hit.",
            "<color=red>Fullness</color>, decrease when not eating food.",
            "<color=red>Sanity</color>, decrease when hunt and scout.",
        }
    };

    public void showExplain(int type, int id)
    {
        //Debug.Log("show");
        //time = 0;
        //StopAllCoroutines();
        if (GameTurnManager.Instance.currentTurn == GameTurn.player)
        {
            explainPanel.SetActive(true);
            explainLabel.text = explains[type][id];
        }
    }

    public void hideExplain()
    {
        //if (time < 0.05f)
        //{
        //    return;
        //}
        //Debug.Log("hide");
        //time = 0;
        //StopAllCoroutines();
        //StartCoroutine(delayHide());

        explainPanel.SetActive(false);
        explainLabel.text = "";
    }
    //IEnumerator delayHide()
    //{
    //    //yield return new WaitForSeconds(0.3f);
    //    //if (time < 0.25f)
    //    //{
    //    //    yield break;
    //    //}
    //    explainPanel.SetActive(false);
    //    explainLabel.text = "";
    //}

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
    }
}
