using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable object/Item")]
public class Item : ScriptableObject
{

    [SerializeField] private Sprite _sprite;
    [SerializeField] private ItemType _itemType;
    [SerializeField] private string _itemName;
    [SerializeField] private string _itemDescription;
    [SerializeField] private int _id;
    private bool _isStackable = true;
    
    
    
    
    public enum ItemType
    {
        Consumable,
        Equipment,
        Miscellaneous
    }

    public Sprite ItemSprite
    {
        get { return _sprite; }
    }
    
    public string ItemName
    {
        get { return _itemName; }
    }
    
    public int ItemID
    {
        get { return _id; }
    }
    
    public ItemType ItemTypeCategory
    {
        get { return _itemType; }
    }
    public string ItemDescription
    {
        get { return _itemDescription; }
    }

    public bool IsStackable()
    {
        switch (_itemType)
        {
            case ItemType.Consumable:
                return true;
            case ItemType.Equipment:
                return false;
            case ItemType.Miscellaneous:
                return false;
        }

        return _isStackable;
    }



}