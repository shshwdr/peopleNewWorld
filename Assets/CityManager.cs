using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;
using Pool;

public class CityInfo
{
    public string name;
    public int px;
    public int py;
    public int[] collectable;
    //List<>
}

public class AllCityInfo
{
    public List<CityInfo> allCity;
}
public class CityManager : Singleton<CityManager>
{
    List<CityInfo> allCity;
    int currentCityId = 0;
    // Start is called before the first frame update
    void Awake()
    {
        string text = Resources.Load<TextAsset>("json/city").text;
        //data = JsonMapper.ToObject(text);
        var allCities = JsonMapper.ToObject<AllCityInfo>(text);
        allCity = allCities.allCity;
        //AllCityInfo allCityInfoList = JsonUtility.FromJson<AllCityInfo>(text);
        //Debug.Log(allCity.allCity[0].collectable);
    }

    //public void collectItemsFromCurrentCity(int[] collects)
    //{
    //    //allCity[currentCityId].collectable  = Utils.arrayAggregasion(allCity[currentCityId].collectable, collects,-1);

    //    EventPool.Trigger("updateCityResource");
    //}

    public CityInfo currentCityInfo()
    {
        return allCity[currentCityId];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
