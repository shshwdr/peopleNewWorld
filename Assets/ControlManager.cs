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

    public void createPopupUI(string text,Vector3 position)
    {
        var go = Instantiate(popupUI);
        go.GetComponentInChildren<TMP_Text>().text = text;
        go.transform.position = position;
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
