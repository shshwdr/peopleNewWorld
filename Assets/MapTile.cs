using PixelCrushers.DialogueSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapTile : MonoBehaviour
{
    public MapTileType type;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void OnMouseDown()
    {
        if (ControlManager.Instance.shouldBlockMouse()) return;
        if (GameTurnManager.Instance.currentTurn == GameTurn.scout) return;
        if (CityManager.Instance.isCurrentTile(gameObject))
        {

            DialogueManager.ShowAlert("Can not move to same place.");
            //Debug.Log("cant move to same city");
            return;
        }
        if(type == MapTileType.city)
        {
            Popup.Instance.Init("Do you want to move to the new place?", () =>
            {
                CityManager.Instance.moveToCity(gameObject);
                GameTurnManager.Instance.move();
                ScoutTurnView.Instance.moveToCity(gameObject);
            });
        }
        else
        {
            DialogueManager.ShowAlert("Can only move to tent.");
            Debug.Log("only move to city");
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
