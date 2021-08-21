using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ControlManager : Singleton<ControlManager>
{
    public bool shouldBlockInput;
    public GameObject popupUI;
    public bool shouldBlockMouse()
    {
        return shouldBlockInput;
    }

    public void createPopupUI(string text,Vector3 position, Color color)
    {
        if (popupUI == null)
        {
            popupUI = null;
        }
        var go = Instantiate(popupUI,transform);
        go.GetComponentInChildren<TMP_Text>().text = text;
        go.transform.position = position;
        go.GetComponentInChildren<TMP_Text>().color = color;
    }

    public void cleanPopups()
    {
        Utils.destroyAllChildren(transform);
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
