using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoutTurnView : TurnView
{
    //MonsterGroup[] monsterGroups;
    public GameObject map;
    public GameObject cancelButton;
    //MonsterGroup selectedGroup;
    public override void startTurnView()
    {
        
        base.startTurnView();
        //hideRelatedCharacters();
        showScoutMap();
        map.SetActive(true);
        cancelButton.SetActive(true);
    }

    protected override void setCharactersPosition()
    {
        if (relatedCharacters == null)
        {
            relatedCharacters = CharacterManager.Instance.getCharacters();
        }
        for (int i = 0; i < relatedCharacters.Count; i++)
        {
            var character = relatedCharacters[i];
            character.transform.position = CityManager.Instance.worldPositionOfCurrentBase();
            character.gameObject.SetActive(true);
        }
    }

    void showScoutMap()
    {

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
