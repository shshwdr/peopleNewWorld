using Pool;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CollectableRaw : MonoBehaviour
{
    public InventoryItem item;
    public TMP_Text staticLabel;
    public TMP_Text valueLabel;
    // Start is called before the first frame update
    void Start()
    {
        staticLabel.text = Inventory.Instance.inventoryNameMap[item];
        updateCityResource();
        //if (Inventory.Instance.getItemAmount(item) == 0)
        //{
        //    gameObject.SetActive(false);
        //}
        EventPool.OptIn("updateCityResource", updateCityResource);
    }

    void updateCityResource()
    {
        int value = CityManager.Instance.currentCityInfo().collectable[(int)item];
        valueLabel.text = value.ToString();
        //if (value != 0 /*&& !gameObject.active*/)
        //{
        //    gameObject.SetActive(true);
        //}
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
