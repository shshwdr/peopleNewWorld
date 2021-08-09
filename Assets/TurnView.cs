using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TurnView:MonoBehaviour
{
    public GameTurn gameTurn;
    public Transform characterPositionParent;
    public GameObject uiPanel;
    public GameObject view;
    public TMP_Text descriptionText;

    protected List<Character> relatedCharacters;
    public virtual void startTurnView()
    {
        setCharactersPosition();
        uiPanel.SetActive(true);
        view.SetActive(true);
        updateDescriptionText();
    }

    public virtual void stopTurnView()
    {
        hideAllCharacters();
        uiPanel.SetActive(false);
        view.SetActive(false);
    }

    protected virtual void updateDescriptionText()
    {

    }
    public virtual bool shouldPlayTurn()
    {
        relatedCharacters = CharacterManager.Instance.getCharacters();
        return relatedCharacters.Count > 0;
    }
    void hideAllCharacters()
    {
        for (int i = 0; i < relatedCharacters.Count; i++)
        {
            var character = relatedCharacters[i];
            character.gameObject.SetActive(false);
        }
    }
    protected virtual void setCharactersPosition()
    {
        if (relatedCharacters == null)
        {
            relatedCharacters = CharacterManager.Instance.getCharacters();
        }
        for (int i = 0; i < relatedCharacters.Count; i++)
        {
            var character = relatedCharacters[i];
            character.transform.position = characterPositionParent.GetChild(i).position;
            character.gameObject.SetActive(true);
        }
    }

}