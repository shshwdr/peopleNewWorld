using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;
using Pool;
using UnityEngine.UI;

public class CityIncrease
{

    public float[] collectable;
    public float[] monsters;
}
public class CityInfo
{
    public int id;
    public string name;
    public int px;
    public int py;
    public int[] collectable;
    public int[] monsters;
    public int[] maxMonsterNumber;
    public bool isDestination;


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
    List<CityInfo> allCityOrigin;
    List<CityIncrease> allCityIncrease;

    float resouceRegenerateRate = 1f / 20f;

    public Dictionary<Vector2, CityInfo> keyToCity = new Dictionary<Vector2, CityInfo>();
    int currentCityId = 0;

    public Transform mapTilesParent;
    public int mapTileWidth = 7;
    public int mapTileHeight = 4;
    Dictionary<Vector2, bool> isMapTileUnlocked = new Dictionary<Vector2, bool>();
    public Dictionary<GameObject, Vector2> mapTileToKey = new Dictionary<GameObject, Vector2>();
    Dictionary<Vector2, GameObject> keyToMapTile = new Dictionary<Vector2, GameObject>();

    public Sprite tentIcon;
    public Sprite[] tileIcons;

    public List<MonsterRow> monsterRow = new List<MonsterRow>();

    // Start is called before the first frame update
    void Awake()
    {
        string text = Resources.Load<TextAsset>("json/city").text;
        //data = JsonMapper.ToObject(text);
        var allCities = JsonMapper.ToObject<AllCityInfo>(text);
        allCity = allCities.allCity;

        var allCities2= JsonMapper.ToObject<AllCityInfo>(text);
        allCityOrigin = allCities2.allCity;
        allCityIncrease = new List<CityIncrease>();
        int i = 0;
        foreach(var cityInfo in allCity)
        {
            cityInfo.id = i;
            keyToCity[new Vector2(cityInfo.px, cityInfo.py)] = cityInfo;
            i++;
            var increase = new CityIncrease();
            increase.collectable = new float[cityInfo.collectable.Length];
            increase.monsters = new float[cityInfo.monsters.Length];
            allCityIncrease.Add(increase);
        }

    }

    public void regenerateAllResources()
    {
        for(int i = 0; i < allCityIncrease.Count; i++)
        {

            for (int j= 0; j < allCityOrigin[i].collectable.Length;j++)
            {
                allCityIncrease[i].collectable[j] += allCityOrigin[i].collectable[j] * resouceRegenerateRate;
                if (allCityIncrease[i].collectable[j] >= 1)
                {
                    int addValue = Mathf.FloorToInt(allCityIncrease[i].collectable[j]);
                    allCity[i].collectable[j] += addValue;
                    allCity[i].collectable[j] = Mathf.Min(allCity[i].collectable[j], allCityOrigin[i].collectable[j]);
                    allCityIncrease[i].collectable[j] -= addValue;
                }
            }
        }

        for (int i = 0; i < allCityIncrease.Count; i++)
        {

            for (int j = 0; j < allCityOrigin[i].monsters.Length; j++)
            {
                allCityIncrease[i].monsters[j] += allCityOrigin[i].monsters[j] * resouceRegenerateRate;
                if (allCityIncrease[i].monsters[j] >= 1)
                {
                    int addValue = Mathf.FloorToInt(allCityIncrease[i].monsters[j]);
                    allCity[i].monsters[j] += addValue;
                    allCity[i].monsters[j] = Mathf.Min(allCity[i].monsters[j], allCityOrigin[i].monsters[j]);
                    allCityIncrease[i].monsters[j] -= addValue;
                }
            }
        }

        EventPool.Trigger("updateCityResource");
        EventPool.Trigger("updateCityMonster");
    }

    public CityInfo currentCityInfo()
    {
        return allCity[currentCityId];
    }
    public void moveToCity(GameObject go)
    {

        var pos = mapTileToKey[go];
        moveToCity(pos);
    }
    public void moveToCity(Vector2 pos)
    {
        foreach(var row in monsterRow)
        {
            row.gameObject.SetActive(true);
        }

        currentCityId = keyToCity[pos].id;
        if (keyToCity[pos].isDestination)
        {

            CSDialogueManager.Instance.addDialogue(5);
        }

        else if (keyToCity[pos].name.Contains("next town"))
        {

            CSDialogueManager.Instance.addDialogue(8);
        }
        else if (keyToCity[pos].name.Contains("rest town") || keyToCity[pos].name.Contains("center town"))
        {

            CSDialogueManager.Instance.addDialogue(12);
        }
        EventPool.Trigger("updateCityResource");
        EventPool.Trigger("updateCityMonster");
    }

    public bool isCurrentTile(GameObject go)
    {
        if(go == keyToMapTile[currentCityInfo().position])
        {
            return true;
        }
        return false;
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
                    mapTile.GetComponent<MapTile>().type = MapTileType.city;
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
                    mapTile.GetComponent<MapTile>().Hide();
                }
                z++;
            }
        }



        int total = 0;
        foreach (var value in allCity[currentCityId].monsters)
        {
            total += value;
        }
        if (total < 4)
        {
            TutorialManager.Instance.showTutorialPanel(TutorialManager.tutorialAlert_NoAreaMonsterAlert);
        }


        total = 0;
        foreach (var value in allCity[currentCityId].collectable)
        {
            total += value;
        }
        if (total < 8)
        {
            TutorialManager.Instance.showTutorialPanel(TutorialManager.tutorialAlert_NoAreaResourceAlert);
        }
    }

    public void unlockWholeMap()
    {
        foreach(Transform child in mapTilesParent)
        {
            //child.gameObject.SetActive(true);
            child.GetComponent<MapTile>().Show();
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

        //keyToMapTile[key].SetActive(true);

        keyToMapTile[key].GetComponent<MapTile>().Show();


        return getTileMapByKey(key);
    }

    public Vector3 worldPositionOfKey(Vector2 keyPosition)
    {
        return keyToMapTile[keyPosition].transform.position;
    }

    public void killedMonster(int index)
    {
        allCity[currentCityId].monsters[index] -= 1;

        EventPool.Trigger("updateCityMonster");

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
