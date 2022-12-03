using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{

    public InventorySlot[] inventorySlots;
    [SerializeField] private GameObject _inventoryItemPrefab;
    private Item _item;
    
    [SerializeField] private TextMeshProUGUI _itemNameText;
    [SerializeField] private TextMeshProUGUI _itemDescText;
    
    public void SelectItem(Item newItem)
    {
        _item = newItem;
        ShowInfo();
    }

    public void ShowInfo()
    {
        _itemNameText.text = _item.ItemName;
        _itemDescText.text = _item.ItemDescription;
    }
    
    

    public bool AddItem(Item item)
    {
        //check stack
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            InventorySlot slot = inventorySlots[i];
            InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();

            if (itemInSlot != null && itemInSlot.Item == item && itemInSlot.ItemCount < 4 && itemInSlot.Item.IsStackable())
            {
                itemInSlot.ItemCount++;
                itemInSlot.CountItem();
                Debug.Log($"Stacked {item.ItemName}");
                SelectItem(item);
                return true;
            }
        }
        
        //find empty slot
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            InventorySlot slot = inventorySlots[i];
            InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
            
            if (itemInSlot == null)
            {
                
                Debug.Log($"Added {item.ItemName} in new Slot");
                SpawnItem(item,slot);
                SelectItem(item);
                return true;
            }
        }

        return false;
        
    }
    void SpawnItem(Item item, InventorySlot slot)
    {
        GameObject newItem = Instantiate(_inventoryItemPrefab, slot.transform);
        InventoryItem inventoryItem = newItem.GetComponent<InventoryItem>();
        inventoryItem.InitialiseItem(item);
    }
    

}