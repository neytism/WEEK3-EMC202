using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AddItemButton : MonoBehaviour
{
    public TMP_InputField ItemID;
    public InventoryManager inventoryManager;
    public Item[] itemsToAdd;

    
    public void AddItem()
    {
        int id = int.Parse(ItemID.text);
        if (id > itemsToAdd.Length || id < 0)
        {
            
            Debug.Log("Invalid ID");
        }
        else
        {
            bool isAvailable = inventoryManager.AddItem(itemsToAdd[id]);

            if (!isAvailable)
            {
                Debug.Log("No Space Available");
            }
        }
        
        
        
    }
}