using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoCharacterGeneration : MonoBehaviour
{
    AutoBodypartSwap[] bodyParts;
    // Start is called before the first frame update
    void Awake()
    {
        bodyParts = GetComponentsInChildren<AutoBodypartSwap>();
    }

    private void Start()
    {
        var generateIds = CharacterGenerator.Instance.getGenerateIds(this);
        generate(generateIds);
    }

    public int bodyPartsSize()
    {
        return bodyParts.Length;
    }

    public int bodyPartOptionSize(int i)
    {
        return bodyParts[i].options.Count;
    }

    public void generate(List<int> ids)
    {
        for(int i = 0;i<bodyPartsSize();i++)
        {
            bodyParts[i].generate(ids[i]);
        }
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
