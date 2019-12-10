using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory Instance { get; private set; }
    private List<InventoryItem> inventoryItems = new List<InventoryItem>();

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        Debug.Log(inventoryItems.Count);
    }

    public void AddInventoryItem(InventoryItem inventoryItem)
    {
        if (!inventoryItem.item.countable || inventoryItems.Count(i => i.item.name == inventoryItem.item.itemName) == 0)
            inventoryItems.Add(inventoryItem);
        else
        {
            inventoryItems.First(i => i.item.name == inventoryItem.item.itemName).count++;
        }
    }
}