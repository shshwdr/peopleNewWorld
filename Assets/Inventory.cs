using Pool;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum InventoryItem { processedFood, rawFood, poisonedFood, weapon, }
public class Inventory : Singleton<Inventory>
{
    public Dictionary<InventoryItem, string> inventoryNameMap = new Dictionary<InventoryItem, string>()
    {
        {InventoryItem.processedFood,"Processed Food" },
        {InventoryItem.rawFood,"Raw Food" },
        {InventoryItem.poisonedFood,"Poisoned Food" },
        {InventoryItem.weapon,"Weapon" },
    };
    Dictionary<InventoryItem, int> invenrotyAmount;
    // Start is called before the first frame update
    void Awake()
    {
        invenrotyAmount = new Dictionary<InventoryItem, int>();
        invenrotyAmount[InventoryItem.processedFood] = 20;
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
