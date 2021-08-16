using Pool;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ActionSelection : MonoBehaviour
{
    Character character;
    public Dictionary<CharacterAction, string> actionNameMap = new Dictionary<CharacterAction, string>()
    {{CharacterAction.collect, "Collect"},
        {CharacterAction.hunt, "Hunt"},
        {CharacterAction.scout, "Scout"},
        {CharacterAction.rest, "Rest"},
        {CharacterAction.cook, "Cook"},
        {CharacterAction.forge, "Forge"}
    };
    public Transform actionButtonParent;
    public GameObject actionButtonPrefab;
    public TMP_Text currentActionLabel;
    public GameObject allSelectionUI;
    private void Awake()
    {
        character = GetComponent<Character>();
    }
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < System.Enum.GetValues(typeof(CharacterAction)).Length; i++)
        {
            GameObject go = Instantiate(actionButtonPrefab, actionButtonParent);
            go.GetComponent<ActionButton>().Init(i, this);
            if (MainGameManager.Instance.unlockedAction[i])
            {
            }
            else
            {
                go.SetActive(false);
            }
        }
        EventPool.OptIn<int>("mouseDownCharacter", getMouseDown);
    }

    

    private void OnMouseDown()
    {
        if (ControlManager.Instance.shouldBlockMouse())
        {
            return;
        }
        EventPool.Trigger<int>("mouseDownCharacter", character.id);
        //Debug.Log("on mouse down " + character.name);
        actionButtonParent.gameObject.SetActive(true);
        ControlManager.Instance.shouldBlockInput = true;

        for (int i = 0; i < System.Enum.GetValues(typeof(CharacterAction)).Length; i++)
        {
            GameObject go = actionButtonParent.GetChild(i).gameObject;
            if (MainGameManager.Instance.unlockedAction[i])
            {
                go.SetActive(true);
            }
            else
            {
                go.SetActive(false);
            }
        }

    }

    void getMouseDown(int i)
    {
        if (i != character.id)
        {
            hideSelection();
        }
    }

    public void hideSelection()
    {
        StartCoroutine(delayHide());
        //actionButtonParent.gameObject.SetActive(false);
    }
    IEnumerator delayHide()
    {
        yield return new WaitForSeconds(0.1f);
        actionButtonParent.gameObject.SetActive(false);

    }
    public void hideAllSelectionUI()
    {
        allSelectionUI.SetActive(false);
    }
    public void showAllSelectionUI()
    {
        allSelectionUI.SetActive(true);
    }

   

    public void selectAction(int i)
    {
        ControlManager.Instance.shouldBlockInput = false;
        if (character.canSelectAction(i))
        {

            character.setAction(i);
            hideSelection();
        }
    }

    public void updateCurrentAction()
    {
        currentActionLabel.text = actionNameMap[character.currentAction];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
