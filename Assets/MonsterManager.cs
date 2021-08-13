using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;
using Pool;

public class MonsterInfo
{
    public string name;
    public int maxHP;
    public int attack;
    public float basicHitRate;
    public float basicAvoidRate;
    public int[] reward;
}

public class AllMonsterInfo
{
    public List<MonsterInfo> allMonsters;
}
public class MonsterManager : Singleton<MonsterManager>
{
    List<MonsterInfo> allMonster;
    // Start is called before the first frame update
    void Awake()
    {
        string text = Resources.Load<TextAsset>("json/monster").text;
        //data = JsonMapper.ToObject(text);
        var allCities = JsonMapper.ToObject<AllMonsterInfo>(text);
        allMonster = allCities.allMonsters;
    }

    public MonsterInfo getMonsterInfo(int i)
    {
        return allMonster[i];
    }
}
