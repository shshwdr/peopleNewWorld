using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlManager : Singleton<ControlManager>
{
    public bool shouldBlockInput;

    public bool shouldBlockMouse()
    {
        return shouldBlockInput;
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
