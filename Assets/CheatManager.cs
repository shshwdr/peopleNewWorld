using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheatManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            for(int i = 0;i< MainGameManager.Instance.unlockedAction.Length; i++)
            {
                MainGameManager.Instance.unlockedAction[i] = true;
            }
        }
    }
}
