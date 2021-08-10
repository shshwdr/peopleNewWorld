using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum CharacterAction { collect,rest};
public class Character : MonoBehaviour
{
    public int id;
    public CharacterAction currentAction;
    public ActionSelection actionSelection;

    public string name;
    public void Init(int i,string n)
    {
        id = i;
        name = n;
    }
    public void setAction(int i)
    {
        currentAction = (CharacterAction)i;
        actionSelection.updateCurrentAction();
    }
    private void Awake()
    {
        actionSelection = GetComponent<ActionSelection>();
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
