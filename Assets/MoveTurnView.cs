using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTurnView : TurnView
{
    public GameObject map;
    public GameObject characters;

    //public void showMap()
    //{
    //    map.SetActive(!map.active);
    //    CharacterManager.Instance.characterParent.SetActive(!CharacterManager.Instance.characterParent.active);
    //}
    public override void startTurnView()
    {
        base.startTurnView();
        hideMap();
    }


    public void hideMap()
    {
        MapController.Instance.openMap();
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
