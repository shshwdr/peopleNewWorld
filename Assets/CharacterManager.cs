using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : Singleton<CharacterManager>
{
    public GameObject characterPrefab;
    List<Character> characterList = new List<Character>();
    GameObject characterParent;

    void Awake()
    {
        characterParent = new GameObject("characterParent");
    }
    public void addCharacter()
    {
        //Transform position = characterPositionParent.GetChild(characterList.Count);
        GameObject go = Instantiate(characterPrefab, characterParent.transform);
        characterList.Add(go.GetComponent< Character>());
    }

    List<Character> getCharacterWithAction(CharacterAction action)
    {
        List<Character> res = new List<Character>();
        foreach(Character go in characterList)
        {
            if(go.currentAction == action)
            {
                res.Add(go);
            }
        }
        return res;
    }

    public List<Character> getCharacters()
    {
        switch (GameTurnManager.Instance.currentTurn)
        {
            case GameTurn.player:

                return characterList;
            case GameTurn.collect:
                return getCharacterWithAction((CharacterAction)((int)GameTurnManager.Instance.currentTurn-1));
        }

        return characterList;
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
