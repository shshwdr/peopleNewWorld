using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainGameManager : MonoBehaviour
{
    public int startCharacterNum = 4;
    public GameObject characterPrefab;
    public Transform characterPositionParent;
    // Start is called before the first frame update
    void Start()
    {
        GameObject characterParent = new GameObject("characterParent");
        for(int i = 0; i < startCharacterNum; i++)
        {
            Transform position = characterPositionParent.GetChild(i);
            Instantiate(characterPrefab, position.position, Quaternion.identity, characterParent.transform);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
