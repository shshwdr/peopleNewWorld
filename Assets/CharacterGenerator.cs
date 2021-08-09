using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CharacterGenerator : Singleton<CharacterGenerator>
{
    Dictionary<string, bool> isCharacterGenerated = new Dictionary<string, bool>();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public List<int> getGenerateIds(AutoCharacterGeneration characterGeneration)
    {
        List<int> res = new List<int>();
        int test = 0;
        while (test < 100)
        {
            test++;
            res = new List<int>();
            for (int i = 0; i < characterGeneration.bodyPartsSize(); i++)
            {
                int rand = Random.Range(0, characterGeneration.bodyPartOptionSize(i));
                res.Add(rand);
            }
            string word = res.Select(i => i.ToString()).Aggregate((i, j) => i + " " + j);
            if (!isCharacterGenerated.ContainsKey(word))
            {
                Debug.Log("character generate: "+word);
                isCharacterGenerated[word] = true;
                break;
            }
            else
            {
                Debug.Log("character duplication: "+word);
            }
        }
        return res;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
