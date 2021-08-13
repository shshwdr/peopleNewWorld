using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;
using Pool;
using UnityEngine.UI;

public class CityInfo
{
    public string name;
    public int px;
    public int py;
    public int[] collectable;
    public int[] monsters;
    public int[] maxMonsterNumber;
    public Vector2 position { get { return new Vector2(px, py); } }
    //List<>
}

public class AllCityInfo
{
    public List<CityInfo> allCity;
}
public class CityManager : Singleton<CityManager>
{
    List<CityInfo> allCity;
    Dictionary<Vector2, CityInfo> keyToCity = new Dictionary<Vector2, CityInfo>();
    int currentCityId = 0;

    public Transform mapTilesParent;
    int mapTileWidth = 7;
    int mapTileHeight = 4;
    Dictionary<Vector2, bool> isMapTileUnlocked = new Dictionary<Vector2, bool>();
    Dictionary<GameObject, Vector2> mapTileToKey = new Dictionary<GameObject, Vector2>();
    Dictionary<Vector2, GameObject> keyToMapTile = new Dictionary<Vector2, GameObject>();

    public Sprite tentIcon;

    // Start is called before the first frame update
    void Awake()
    {
        string text = Resources.Load<TextAsset>("json/city").text;
        //data = JsonMapper.ToObject(text);
        var allCities = JsonMapper.ToObject<AllCityInfo>(text);
        allCity = allCities.allCity;

        foreach(var cityInfo in allCity)
        {
            keyToCity[new Vector2(cityInfo.px, cityInfo.py)] = cityInfo;
        }

    }

    public CityInfo currentCityInfo()
    {
        return allCity[currentCityId];
    }

    public void generateMap()
    {
        int z = 0;
        for(int i = 0;i< mapTileWidth; i++)
        {
            for(int j = 0;j< mapTileHeight; j++)
            {
                GameObject mapTile =  mapTilesParent.GetChild(z).gameObject;
                Vector2 key = new Vector2(i, j);
                mapTileToKey[mapTile] = key;
                keyToMapTile[key] = mapTile;

                if (keyToCity.ContainsKey(key))
                {
                    mapTile.GetComponent<Image>().sprite = tentIcon;
                }

                if (isMapTileUnlocked.ContainsKey(key))
                {

                }
                else
                {
                    //mapTile.SetActive(false);
                }
                z++;
            }
        }
    }

    private void Start()
    {
        isMapTileUnlocked[Vector2.zero] = true;

        generateMap();
    }

    public Vector3 worldPositionOfCurrentBase()
    {
        Vector2 canvasPosition = allCity[currentCityId].position;
        return Camera.main.ScreenToWorldPoint(canvasPosition);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
