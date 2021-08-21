using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using PixelCrushers.DialogueSystem;

public enum MapTileType { dessert,swamp,beach, water,city}
public class ScoutTurnView : TurnView
{

    //MonsterGroup[] monsterGroups;
    public GameObject map;
    public GameObject cancelButton;
    public Transform directionsParent;
    bool hasStartedAScout;
    public int lossSanity = 25;
    MapTileType currentMapTileType;
    public Character scoutCharacter;

    public MapController mapController;

    public Vector2 currentScoutKeyPosition = Vector2.negativeInfinity;
    //MonsterGroup selectedGroup;

    public Transform backgrounds;

    public void setAction(Character chara, int i)
    {
        if (chara == scoutCharacter && i != (int)CharacterAction.scout)
        {
            scoutCharacter = null;
        }
        if(!scoutCharacter && i == (int)CharacterAction.scout)
        {
            scoutCharacter = chara;
        }
    }
    public bool canCharacterScout(Character chara)
    {
        if (scoutCharacter)
        {
            DialogueManager.ShowAlert("Only one person can scout at a time.");
            return false;
        }
        return true;
    }

    public override void startTurnView()
    {

        if (hasStartedAScout)
        {
           // keyPosition = currentScoutKeyPosition;
        }
        else
        {
            currentScoutKeyPosition = CityManager.Instance.keyPositoinOfCurrentBase();
        }

        setCharactersPosition();
        //uiPanel.SetActive(true);
        view.SetActive(true);
        //base.startTurnView();
        //hideRelatedCharacters();
        mapController.openMap();


        int res = (int)currentMapTileType;
        if (!hasStartedAScout)
        {
            res = 4;
        }
        else
        {
            if (currentMapTileType == MapTileType.city)
            {
                var cityInfo = CityManager.Instance.keyToCity[currentScoutKeyPosition];
                if (cityInfo.isDestination)
                {
                    res += 1;
                }
            }
        }

        uiPanel.SetActive(true);
        updateBackground(res);


        nextButton.SetActive(false);
        cancelButton.SetActive(true);
        descriptionText.text = "Select a direction to move or cancel to return base.";
        //map.SetActive(true);
        //if (!hasStartedAScout)
        //{
        //    cancelButton.SetActive(true);
        //}
        showDirections();

        TutorialManager.Instance.showTutorialPanel(TutorialManager.tutorialTurnIntro_Scout);
    }

    public void onClickCancelButton()
    {
        //show pop up
        Popup.Instance.Init("Do you want to cancel the scout and return to the base?", () =>
        {
            gameOver(10);

            mapController.closeMap();
            cancelCurrentScout();
        });
        //Debug.Log("cancel");
        //stopTurnView();
    }
    void cancelCurrentScout()
    {
        hasStartedAScout = false;
        scoutCharacter.isScouting = false;
       // scoutCharacter = null;
    }
    public void moveToCity(GameObject cityOb)
    {
        Vector2 cityPosition = CityManager.Instance.mapTileToKey[cityOb];
        if(hasStartedAScout && cityPosition == currentScoutKeyPosition)
        {
            cancelCurrentScout();
        }

        uiPanel.SetActive(false);
    }

    public void showDirections()
    {
        directionsParent.gameObject.SetActive(true);

        directionsParent.transform.position = CityManager.Instance.worldPositionOfKey(currentScoutKeyPosition);
        for (int i = 0; i < 4; i++)
        {
            GameObject dir =  directionsParent.GetChild(i).gameObject;
            dir.SetActive(true);
            switch (i)
            {
                case 0:
                    if (currentScoutKeyPosition.x >= CityManager.Instance.mapTileWidth-1)
                    {
                        dir.SetActive(false);
                        continue;
                    }
                    break;
                case 1:
                    if (currentScoutKeyPosition.y<=0)
                    {
                        dir.SetActive(false);
                        continue;
                    }
                    break;
                case 2:
                    if (currentScoutKeyPosition.x <=0)
                    {
                        dir.SetActive(false);
                        continue;
                    }
                    break;
                case 3:
                    if (currentScoutKeyPosition.y >= CityManager.Instance.mapTileHeight-1)
                    {
                        dir.SetActive(false);
                        continue;
                    }
                    break;
            }
            var nextPosition = currentScoutKeyPosition + dirIntToVector2(i);
            if(CityManager.Instance.getTileMapByKey(nextPosition) == MapTileType.water)
            {
                if (MainGameManager.Instance.unlockedItem[1])
                {
                    continue;
                }
                else
                {

                    dir.SetActive(false);
                    continue;
                }
            }
        }
    }

    Vector2 dirIntToVector2(int d)
    {
        switch (d)
        {
            case 0:
                return new Vector2(1, 0);
            case 1:
                return new Vector2(0, -1);
            case 2:
                return new Vector2(-1, 0);
            case 3:
                return new Vector2(0, 1);
        }
        return Vector2.zero;
    }

    public void moveTo(int dir) {
        //0 right,1 up, 2 left, 3 down
        cancelButton.SetActive(false);
        hasStartedAScout = true;
        directionsParent.gameObject.SetActive(false);
        scoutCharacter.isScouting = true;

        StartCoroutine(delayMove(dir));
    }

    IEnumerator delayMove(int dir)
    {


        Vector2 nextKeyPosition = currentScoutKeyPosition + dirIntToVector2(dir);
        Vector3 nextWorldPosition = CityManager.Instance.worldPositionOfKey(nextKeyPosition);
        relatedCharacters[0].transform.DOMove(nextWorldPosition, 1);
        yield return new WaitForSeconds(1);
        currentMapTileType = CityManager.Instance.unlockTileMapByKey(nextKeyPosition);


        currentScoutKeyPosition = nextKeyPosition;
        hideRelatedCharacters();
        showRelatedCharacters();
        
        mapController.closeMap();
        setCharactersPosition();
        map.SetActive(false);



        int res = (int)currentMapTileType;
        if (!hasStartedAScout)
        {
            res = 4;
        }
        else
        {
            if (currentMapTileType == MapTileType.city)
            {
                var cityInfo = CityManager.Instance.keyToCity[currentScoutKeyPosition];
                if (cityInfo.isDestination)
                {
                    res += 1;
                }
            }
        }
        updateBackground(res);


        StartCoroutine(moveCharacters());

        //gameOver((int)type);
    }

    protected override void afterMoveCharacter()
    {
        base.afterMoveCharacter();

        for (int i = 0; i < relatedCharacters.Count; i++)
        {
            relatedCharacters[i].showStatus(CharacterStatus.sanity);
            int sanity = lossSanity;
            if (MainGameManager.Instance.unlockedItem[0])
            {
                sanity = sanity * 2 / 3;
            }
            relatedCharacters[i].decreaseStatus(CharacterStatus.sanity, sanity);
        }
        gameOver((int)currentMapTileType);

    }

    void updateBackground(int res)
    {
        foreach(Transform b in backgrounds)
        {
            b.gameObject.SetActive(false);
        }
        if (res != 10)
        {
            backgrounds.GetChild(res).gameObject.SetActive(true);
        }
        else
        {

            backgrounds.GetChild(backgrounds.childCount-1).gameObject.SetActive(true);
        }
    }
    void gameOver(int res)
    {
        
        if(currentMapTileType == MapTileType.city)
        {
            var cityInfo = CityManager.Instance.keyToCity[currentScoutKeyPosition];
            if (cityInfo.isDestination)
            {

                CSDialogueManager.Instance.addDialogue(4);
                res += 1;
            }
        }
        updateBackground(res);
        if (res == 0)
        {
            //all character die
            descriptionText.text = "Scout a dessert place, not suitable to live... Keep going!";
        }
        else if (res == 1)
        {
            // all enemy die
            descriptionText.text = relatedCharacters[0].name + " scout a swamp place, not suitable to live...";
        }
        else if (res == 2)
        {
            // all enemy die
            descriptionText.text = relatedCharacters[0].name + " scout a beach, nice place but there are water blocks out way...";

            CSDialogueManager.Instance.addDialogue(9);
            TutorialManager.Instance.showTutorialPanel(TutorialManager.tutorialAlert_WaterAlert);
        }
        else if (res == 3)
        {
            // all enemy die
            descriptionText.text = relatedCharacters[0].name + " scout a water, thanks for the boat we made we can move across water now...";
        }
        else if (res == 4)
        {
            // all enemy die
            descriptionText.text = relatedCharacters[0].name +" found a plain place that the team can live!";

            TutorialManager.Instance.unlockAction((int)CharacterAction.rest + 1);

            CSDialogueManager.Instance.addDialogue(3);
        }
        else if (res == 5)
        {
            // all enemy die
            descriptionText.text = relatedCharacters[0].name + " found the place with the fuel we need!";

            TutorialManager.Instance.unlockAction((int)CharacterAction.rest + 1);

            CSDialogueManager.Instance.addDialogue(3);
        }
        else if (res == 10)
        {
            //cancel
            descriptionText.text = relatedCharacters[0].name+" returned back to the base.";
        }

        if (relatedCharacters[0].getStatus(CharacterStatus.sanity) < MainGameManager.Instance.forceRestSanity)
        {
            descriptionText.text += "Sanity too low to keep scouting.";
            TutorialManager.Instance.showTutorialPanel(TutorialManager.tutorialAlert_SanityAlertScout);
            cancelCurrentScout();
        }

        directionsParent.gameObject.SetActive(false);

        cancelButton.SetActive(false);
        nextButton.SetActive(true);

    }


    public override void stopTurnView()
    {
        base.stopTurnView();
        for (int i = 0; i < relatedCharacters.Count; i++)
        {
            var character = relatedCharacters[i];
            character.transform.localScale = new Vector3(1f, 1f, 1);
        }
        for(int  i = 0; i < backgrounds.childCount-1; i++)
        {
            backgrounds.GetChild(i).gameObject.SetActive(false);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    // Check to see if we're about to be destroyed.
    private static bool m_ShuttingDown = false;
    private static object m_Lock = new object();
    private static ScoutTurnView m_Instance;

    /// <summary>
    /// Access singleton instance through this propriety.
    /// </summary>
    public static ScoutTurnView Instance
    {
        get
        {
            //if (m_ShuttingDown)
            //{
            //    Debug.LogWarning("[Singleton] Instance '" + typeof(T) +
            //        "' already destroyed. Returning null.");
            //    return null;
            //}

            lock (m_Lock)
            {
                if (m_Instance == null)
                {
                    // Search for existing instance.
                    m_Instance = (ScoutTurnView)FindObjectOfType(typeof(ScoutTurnView));

                    // Create new instance if one doesn't already exist.
                    if (m_Instance == null)
                    {
                        // Need to create a new GameObject to attach the singleton to.
                        var singletonObject = new GameObject();
                        m_Instance = singletonObject.AddComponent<ScoutTurnView>();
                        singletonObject.name = typeof(ScoutTurnView).ToString() + " (Singleton)";

                        // Make instance persistent.
                        // DontDestroyOnLoad(singletonObject);
                    }
                }

                return m_Instance;
            }
        }
    }


    private void OnApplicationQuit()
    {
        m_ShuttingDown = true;
    }


    private void OnDestroy()
    {
        m_ShuttingDown = true;
        m_Instance = null;
    }
}
