using Pool;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InventoryRaw : MonoBehaviour
{
    public InventoryItem item;
    public TMP_Text staticLabel;
    public TMP_Text valueLabel;
    // Start is called before the first frame update
    void Start()
    {
        staticLabel.text = Inventory.Instance.inventoryNameMap[item];
        updateInventory();
        if (Inventory.Instance.getItemAmount(item) == 0)
        {
            gameObject.SetActive(false);
        }
        EventPool.OptIn("updateInventory", updateInventory);
    }

    void updateInventory()
    {
        int value = Inventory.Instance.getItemAmount(item);
        valueLabel.text = value.ToString();
        if (value != 0 && !gameObject.active)
        {
            gameObject.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
