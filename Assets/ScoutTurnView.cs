using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public enum MapTileType { dessert,swamp,city}
public class ScoutTurnView : TurnView
{
    //MonsterGroup[] monsterGroups;
    public GameObject map;
    public GameObject cancelButton;
    public Transform directionsParent;
    bool hasStartedAScout;
    Vector2 currentScoutKeyPosition = Vector2.negativeInfinity;
    //MonsterGroup selectedGroup;

    public Transform backgrounds;

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
        base.startTurnView();
        //hideRelatedCharacters();
        showScoutMap();
        map.SetActive(true);
        if (!hasStartedAScout)
        {
            cancelButton.SetActive(true);
        }
        showDirections();
    }

    public void onClickCancelButton()
    {
        hasStartedAScout = false;
        gameOver(10);
        //Debug.Log("cancel");
        //stopTurnView();
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
                    }
                    break;
                case 1:
                    if (currentScoutKeyPosition.y<=0)
                    {
                        dir.SetActive(false);
                    }
                    break;
                case 2:
                    if (currentScoutKeyPosition.x <=0)
                    {
                        dir.SetActive(false);
                    }
                    break;
                case 3:
                    if (currentScoutKeyPosition.y >= CityManager.Instance.mapTileHeight-1)
                    {
                        dir.SetActive(false);
                    }
                    break;
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

        StartCoroutine(delayMove(dir));
    }

    IEnumerator delayMove(int dir)
    {


        Vector2 nextKeyPosition = currentScoutKeyPosition + dirIntToVector2(dir);
        Vector3 nextWorldPosition = CityManager.Instance.worldPositionOfKey(nextKeyPosition);
        relatedCharacters[0].transform.DOMove(nextWorldPosition, 1);
        yield return new WaitForSeconds(1);
        MapTileType type = CityManager.Instance.unlockTileMapByKey(nextKeyPosition);
        currentScoutKeyPosition = nextKeyPosition;

        map.SetActive(false);
        gameOver((int)type);
    }
    void gameOver(int res)
    {
        if (res != 10)
        {
            backgrounds.GetChild(res).gameObject.SetActive(true);
        }
        if (res == 0)
        {
            //all character die
            descriptionText.text = "scout a new place, a dessert place, not suitable to live";
        }
        else if (res == 1)
        {
            // all enemy die
            descriptionText.text = "scout a new place, a swamp place, not suitable to live";
        }
        else if (res ==2)
        {
            // all enemy die
            descriptionText.text = "found another city!";
        }
        else if (res == 10)
        {
            //cancel
            descriptionText.text = "Cancelled the scout.";
        }


        cancelButton.SetActive(false);
        nextButton.SetActive(true);

    }

    protected override void setCharactersPosition()
    {
        if (relatedCharacters == null)
        {
            relatedCharacters = CharacterManager.Instance.getCharacters();
        }
        for (int i = 0; i < relatedCharacters.Count; i++)
        {
            var character = relatedCharacters[i];
            var position = CityManager.Instance.worldPositionOfKey(currentScoutKeyPosition);
            character.transform.position = new Vector3(position.x, position.y, character.transform.position .z);
            character.gameObject.SetActive(true);
            character.transform.localScale = new Vector3(0.3f, 0.3f, 1);
            break;
        }
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

    void showScoutMap()
    {

    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}