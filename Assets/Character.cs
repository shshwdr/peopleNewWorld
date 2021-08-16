using Pool;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum CharacterAction { collect, forge, hunt, cook, scout, rest };
public enum CharacterAbility { Int,Dex,Agi,Str };


public enum CharacterStatus { health, hungry, sanity };
public class Character : HPObject
{
    public GameObject[] tempItems;//0 fruit, 1 meat, 2 material, 3 weapon, 4 cooked meat
    public Transform tempItemsParent;
    public Transform tempItemsPosition;
    public Transform popupPositions;
    int popupPositionId;
    int tempItemsId = 0;
    public static Dictionary<CharacterAbility, string> abilityNameMap = new Dictionary<CharacterAbility, string>()
{
    {CharacterAbility.Int,"INT" },
    {CharacterAbility.Dex,"DEX" },
    {CharacterAbility.Agi,"AGI" },
    {CharacterAbility.Str,"STR" },
};

    public HPBar[] statusBar;

    public int id;
    public CharacterAction currentAction;
    public ActionSelection actionSelection;
    DragCharacter dragComponent;
    public bool isScouting = false;

    public int[] abilityValue = new int[4];
    int[] statusValue = new int[3] { 100, 100, 100 };


    public string name;

    AbilityRow[] abilitiesUI;



    public override int attack
    {
        get
        {
            int res = 3;
            //str buff
            res += Mathf.FloorToInt((getAbility(CharacterAbility.Str)) * 0.1f);
            //weapon
            if (Inventory.Instance.getItemAmount(InventoryItem.weapon) > 1)
            {
                res += 5;
                Inventory.Instance.consumeItem(InventoryItem.weapon, 1);
            }
            return res;
        }
    }

    public float avoidRate
    {
        get
        {
            return 1.0f - (getAbility(CharacterAbility.Agi) * 0.008f);
        }
    }

    public float hitRate
    {
        get
        {
            return 1.0f + (getAbility(CharacterAbility.Dex) * 0.01f);
        }
    }

    public void hideStatus()
    {
        foreach(var bar in statusBar)
        {
            bar.gameObject.SetActive(false);
        }
    }
    public void showStatus()
    {
        foreach (var bar in statusBar)
        {
            bar.gameObject.SetActive(true);
        }
    }
    public void showStatus(CharacterStatus status)
    {
        statusBar[(int)status].gameObject.SetActive(true);
    }
    public void updateAbilities()
    {
        foreach(var ui in abilitiesUI)
        {
            ui.updateCityResource();
        }
    }

    public int getAbility(CharacterAbility ability)
    {
        return abilityValue[(int)ability];
    }
    public void increaseAbility(CharacterAbility ability, int val)
    {
        abilityValue[(int)ability] += val;
        abilityValue[(int)ability] = Mathf.Min(abilityValue[(int)ability], 100);
        ControlManager.Instance.createPopupUI(abilityNameMap[ability] + " + " + val.ToString(), popupPositions.GetChild(popupPositionId).position);
        popupPositionId++;
        if(popupPositionId>= popupPositions.childCount)
        {
            popupPositionId = 0;
        }
    }

    public int getStatus(CharacterStatus ability)
    {
        return statusValue[(int)ability];
    }

    public void increaseStatus(CharacterStatus ability, int val)
    {
        statusValue[(int)ability] += val;
        statusValue[(int)ability] = Mathf.Min(abilityValue[(int)ability], 100);
        statusBar[(int)ability].updateCurrentValue(statusValue[(int)ability]);
    }

    

    public void decreaseStatus(CharacterStatus ability, int val)
    {
        statusValue[(int)ability] -= val;
        statusValue[(int)ability] = Mathf.Max(statusValue[(int)ability], 0);
        statusBar[(int)ability].updateCurrentValue(statusValue[(int)ability]);
        if (statusValue[(int)ability] <= 0)
        {
            die();
        }
    }


    public override void doDamage(int damage)
    {
        decreaseStatus(CharacterStatus.health, damage);
    }

    public override void heal(int val)
    {
        increaseStatus(CharacterStatus.health, val);
    }

    public void Init(int i,string n)
    {
        base.Init();
        id = i;
        //name = n;
    }
    
    public void addTempItem(int i)
    {
        GameObject go = Instantiate(tempItems[i], tempItemsParent);
        go.transform.position = tempItemsPosition.GetChild(tempItemsId).position;
        tempItemsId++;

    }
    public void cleanTempItems() {
        tempItemsId = 0;
        Utils.destroyAllChildren(tempItemsParent);
    }


    public void startBattle()
    {
        dragComponent.enabled = true;
        actionSelection.enabled = false;
    }
    public void stopBattle()
    {

        dragComponent.enabled = false;
        actionSelection.enabled = true;
    }

    public bool canSelectAction(int i)
    {

        var tosetAction = (CharacterAction)i;
        if(tosetAction == CharacterAction.scout)
        {
            return ScoutTurnView.Instance.canCharacterScout(this);
        }
        return true;
    }
    public void setAction(int i)
    {


        currentAction = (CharacterAction)i;
        actionSelection.updateCurrentAction();
        ScoutTurnView.Instance.setAction(this,i);
    }
    protected override void Awake()
    {
        base.Awake();
        dragComponent = GetComponent<DragCharacter>();
        actionSelection = GetComponent<ActionSelection>();
        dragComponent.enabled = false;
        abilitiesUI = GetComponentsInChildren<AbilityRow>();
    }
    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < 3; i++)
        {

            statusBar[i].setMaxValue(100);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
