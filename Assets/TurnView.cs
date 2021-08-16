using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class TurnView:MonoBehaviour
{
    public GameTurn gameTurn;
    public Transform characterPositionParent;
    public GameObject uiPanel;
    public GameObject view;
    public TMP_Text descriptionText;
    public GameObject nextButton;


    protected List<Character> relatedCharacters;
    public virtual void startTurnView()
    {
        setCharactersPosition();
        //uiPanel.SetActive(true);
        view.SetActive(true);
        StartCoroutine(moveCharacters());
    }


    public virtual void stopTurnView()
    {
        hideRelatedCharacters();
        for (int i = 0; i < CharacterManager.Instance.characterList.Count; i++)
        {
            var character = CharacterManager.Instance.characterList[i];
            character.cleanTempItems();
        }
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
    public void hideRelatedCharacters()
    {
        for (int i = 0; i < CharacterManager.Instance.characterList.Count; i++)
        {
            var character = CharacterManager.Instance.characterList[i];
            character.hideStatus();
            character.gameObject.SetActive(false);
            character.GetComponent<ActionSelection>().cancelSelection();
        }
    }
    public void showRelatedCharacters()
    {
        for (int i = 0; i < relatedCharacters.Count; i++)
        {
            var character = relatedCharacters[i];
            character.gameObject.SetActive(true);
        }
    }
    public virtual void setCharactersPosition()
    {
        //if (relatedCharacters == null)
        {
            relatedCharacters = CharacterManager.Instance.getCharacters();
        }
        for (int i = 0; i < relatedCharacters.Count; i++)
        {
            var character = relatedCharacters[i];
            character.transform.position = characterPositionParent.GetChild(i).position - new Vector3(20,Random.Range(-10,10),0);
            character.gameObject.SetActive(true);
        }
    }


    protected IEnumerator moveCharacters()
    {
        for (int i = 0; i < relatedCharacters.Count; i++)
        {
            var character = relatedCharacters[i];
            character.transform.DOMove(characterPositionParent.GetChild(i).position, 1);
        }
        yield return new WaitForSeconds(1);
        afterMoveCharacter();

    }

    protected virtual void afterMoveCharacter()
    {
        loadTutorials();
        uiPanel.SetActive(true);

        updateDescriptionText();
    }

    protected virtual void loadTutorials()
    {

    }

}