using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;

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
public class CityManager : MonoBehaviour
{
    AllCityInfo allCity;
    // Start is called before the first frame update
    void Start()
    {
        string text = Resources.Load<TextAsset>("json/city").text;
        //data = JsonMapper.ToObject(text);
        allCity = JsonMapper.ToObject<AllCityInfo>(text);
        //AllCityInfo allCityInfoList = JsonUtility.FromJson<AllCityInfo>(text);
        Debug.Log(allCity.allCity[0].collectable);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
