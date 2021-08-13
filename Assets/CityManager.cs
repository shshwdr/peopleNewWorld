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
    public int mapTileWidth = 7;
    public int mapTileHeight = 4;
    Dictionary<Vector2, bool> isMapTileUnlocked = new Dictionary<Vector2, bool>();
    Dictionary<GameObject, Vector2> mapTileToKey = new Dictionary<GameObject, Vector2>();
    Dictionary<Vector2, GameObject> keyToMapTile = new Dictionary<Vector2, GameObject>();

    public Sprite tentIcon;
    public Sprite[] tileIcons;

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
            for(int j = 0;j< mapTileHeight; j++)
        {
            for (int i = 0; i < mapTileWidth; i++)
            {
                GameObject mapTile =  mapTilesParent.GetChild(z).gameObject;
                Vector2 key = new Vector2(i, j);
                mapTileToKey[mapTile] = key;
                keyToMapTile[key] = mapTile;

                if (keyToCity.ContainsKey(key))
                {
                    mapTile.GetComponent<Image>().sprite = tentIcon;
                }
                else
                {
                    mapTile.GetComponent<Image>().sprite = tileIcons[(int)mapTile.GetComponent<MapTile>().type];
                }

                if (isMapTileUnlocked.ContainsKey(key))
                {

                }
                else
                {
                    mapTile.SetActive(false);
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
    public Vector3 keyPositoinOfCurrentBase()
    {
        return allCity[currentCityId].position;
    }
    public Vector3 worldPositionOfCurrentBase()
    {
        Vector2 canvasPosition = keyToMapTile[ allCity[currentCityId].position].transform.position;
        return canvasPosition;
    }

    public MapTileType getTileMapByKey(Vector2 key)
    {
        if (keyToCity.ContainsKey(key))
        {
            return MapTileType.city;
        }
        return keyToMapTile[key].GetComponent<MapTile>().type;
    }

    public MapTileType unlockTileMapByKey(Vector2 key)
    {
        isMapTileUnlocked[key] = true;

        keyToMapTile[key].SetActive(true);

        

        return getTileMapByKey(key);
    }

    public Vector3 worldPositionOfKey(Vector2 keyPosition)
    {
        return keyToMapTile[keyPosition].transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
