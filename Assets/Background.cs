using Pool;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Background : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseUp()
    {
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            //EventPool.Trigger<int>("mouseDownCharacter", -1);
        }
    }
}
