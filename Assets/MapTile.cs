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
        if (CityManager.Instance.isCurrentTile(gameObject))
        {
            Debug.Log("cant move to same city");
            return;
        }
        if(type == MapTileType.city)
        {
            CityManager.Instance.moveToCity(gameObject);
            GameTurnManager.Instance.move();
        }
        else
        {
            Debug.Log("only move to city");
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
