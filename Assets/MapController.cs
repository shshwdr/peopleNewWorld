using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapController : Singleton<MapController>
{
    public GameObject map;

    Vector2[] moveDir = new Vector2[] { new Vector2(1, -1), new Vector2(1, 1), new Vector2(-1, -1), new Vector2(-1, 1) };
    public float distance = 1;
    public void openMap()
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
                if (ScoutTurnView.Instance.scoutCharacter == character)
                {

                    position = CityManager.Instance.worldPositionOfKey(ScoutTurnView.Instance.currentScoutKeyPosition);
                    position += (Vector3)moveDir[i] * distance;
                }


                character.transform.position = new Vector3(position.x, position.y, character.transform.position.z);
                character.gameObject.SetActive(true);
                character.transform.localScale = new Vector3(0.3f, 0.3f, 1);
            }
        }
        else
        {
            for (int i = 0; i < relatedCharacters.Count; i++)
            {

                var character = relatedCharacters[i];
                character.transform.localScale = new Vector3(1, 1, 1);

            }
            GameTurnManager.Instance.currentTurnView().hideRelatedCharacters();
            GameTurnManager.Instance.currentTurnView().showRelatedCharacters();
            GameTurnManager.Instance.currentTurnView().setCharactersPosition();
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

