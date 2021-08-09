using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnView:MonoBehaviour
{
    public Transform characterPositionParent;
    public GameObject uiPanel;
    public GameObject view;
    public virtual void startTurnView()
    {
        setCharactersPosition();
        uiPanel.SetActive(true);
        view.SetActive(true);
    }

    public virtual void stopTurnView()
    {
        hideAllCharacters();
        uiPanel.SetActive(false);
        view.SetActive(false);
    }

    void hideAllCharacters()
    {
        var characters = CharacterManager.Instance.getCharacters();
        for (int i = 0; i < characters.Count; i++)
        {
            var character = characters[i];
            character.gameObject.SetActive(false);
        }
    }
    protected virtual void setCharactersPosition()
    {
        var characters = CharacterManager.Instance.getCharacters();
        for (int i = 0; i < characters.Count; i++)
        {
            var character = characters[i];
            character.transform.position = characterPositionParent.GetChild(i).position;
            character.gameObject.SetActive(true);
        }
    }

    public virtual void endTurnView()
    {

    }
}