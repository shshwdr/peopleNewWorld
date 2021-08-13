using Pool;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum CharacterAction { collect,hunt,scout,rest};
public class Character : HPObject
{
    public int id;
    public CharacterAction currentAction;
    public ActionSelection actionSelection;
    DragCharacter dragComponent;


    public string name;


    public void Init(int i,string n)
    {
        base.Init();
        id = i;
        name = n;
    }

    public void startBattle()
    {
        dragComponent.enabled = true;
    }
    public void stopBattle()
    {

        dragComponent.enabled = false;
    }
    public void setAction(int i)
    {
        currentAction = (CharacterAction)i;
        actionSelection.updateCurrentAction();
    }
    protected override void Awake()
    {
        base.Awake();
        dragComponent = GetComponent<DragCharacter>();
        actionSelection = GetComponent<ActionSelection>();
        dragComponent.enabled = false;
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
