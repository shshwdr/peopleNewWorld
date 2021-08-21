using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ActionButton : MonoBehaviour
{
    int id;
    ActionSelection selection;

    public void Init(int i, ActionSelection s)
    {
        selection = s;
        id = i;
        GetComponentInChildren<TMP_Text>().text = selection.actionNameMap[(CharacterAction)i];
        GetComponent<Button>().onClick.AddListener(delegate { selection.selectAction(id);
            SFXManager.Instance.playSFXRandom(SFXManager.Instance.clickAction);
        });
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
