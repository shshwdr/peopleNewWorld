using Pool;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum InventoryItem { processedFood, rawFood, /*poisonedFood,*/materials, weapon, }
public class Inventory : Singleton<Inventory>
{
    public Dictionary<InventoryItem, string> inventoryNameMap = new Dictionary<InventoryItem, string>()
    {
        {InventoryItem.processedFood,"Processed Food" },
        {InventoryItem.rawFood,"Raw Food" },
        //{InventoryItem.poisonedFood,"Poisoned Food" },
        {InventoryItem.materials,"Materials" },
        {InventoryItem.weapon,"Weapon" },
    };
    Dictionary<InventoryItem, int> invenrotyAmount;
    // Start is called before the first frame update
    void Awake()
    {
        invenrotyAmount = new Dictionary<InventoryItem, int>();
        invenrotyAmount[InventoryItem.processedFood] = 10;
        invenrotyAmount[InventoryItem.rawFood] = 5;
        invenrotyAmount[InventoryItem.materials] = 5;
    }

    public string inventoryItemsToString(int[] collects)
    {
        string res = "";
        for (int i = 0; i < collects.Length; i++)
        {
            if (collects[i] > 0)
            {
                res += collects[i] + " " + inventoryNameMap[(InventoryItem)i] + " ";
            }
        }
        return res;
    }

    public void addItems(int[] items)
    {
        for(int i = 0; i < items.Length; i++)
        {
            addItem((InventoryItem)i, items[i]);
        }
    }
    public void addItem(InventoryItem item, int value)
    {
        if (!invenrotyAmount.ContainsKey(item))
        {
            invenrotyAmount[item] = 0;
        }
        invenrotyAmount[item] += value;
        EventPool.Trigger("updateInventory");
    }

    public void consumeItem(InventoryItem item, int value)
    {
        invenrotyAmount[item] -= value;
        EventPool.Trigger("updateInventory");
    }

    public int getItemAmount(InventoryItem item)
    {
        if (!invenrotyAmount.ContainsKey(item))
        {
            return 0;
        }
        return invenrotyAmount[item];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
