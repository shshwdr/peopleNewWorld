using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapController : Singleton<MapController>
{
    public GameObject map;

    Vector2[] moveDir = new Vector2[] { new Vector2(1, -1), new Vector2(1, 1), new Vector2(-1, -1), new Vector2(-1, 1) };
    public float distance = 1;

    public void closeMap()
    {
        if (map.active)
        {
            toggleMap();
        }
    }

    public void toggleMap()
    {
        map.SetActive(!map.active);
        var relatedCharacters = CharacterManager.Instance.characterList;
        if (map.active)
        {

            //set character to correct position
            if (relatedCharacters == null)
            {
                relatedCharacters = CharacterManager.Instance.getCharacters();
            }
            for (int i = 0; i < relatedCharacters.Count; i++)
            {



                var position = CityManager.Instance.worldPositionOfCurrentBase();
                position += (Vector3)moveDir[i] * distance;
                var character = relatedCharacters[i];
                if (ScoutTurnView.Instance.scoutCharacter == character && character.isScouting)
                {

                    position = CityManager.Instance.worldPositionOfKey(ScoutTurnView.Instance.currentScoutKeyPosition);
                    position += (Vector3)moveDir[i] * distance;
                }
                character.GetComponent<ActionSelection>().cancelSelection();
                character.GetComponent<ActionSelection>().hideAllSelectionUI();
                character.transform.position = new Vector3(position.x, position.y, character.transform.position.z);
                character.gameObject.SetActive(true);
                character.transform.localScale = new Vector3(0.25f, 0.25f, 1);
            }
        }
        else
        {
            for (int i = 0; i < relatedCharacters.Count; i++)
            {

                var character = relatedCharacters[i];
                character.transform.localScale = new Vector3(1, 1, 1);
                character.GetComponent<ActionSelection>().showAllSelectionUI();

            }
            GameTurnManager.Instance.currentTurnView().hideRelatedCharacters();
            GameTurnManager.Instance.currentTurnView().showRelatedCharacters();
            GameTurnManager.Instance.currentTurnView().setCharactersFinalPosition();
        }
    }
    public void openMap()
    {

        if (!map.active)
        {
            toggleMap();
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

