using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;
using Random = UnityEngine.Random;

public class InventoryItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerClickHandler
{
    
    private Image _image;
    [SerializeField] private TextMeshProUGUI _countText;
    [SerializeField] private TextMeshProUGUI _itemNameText;
    [SerializeField] private TextMeshProUGUI _itemDescText;
    [SerializeField] private Item _item;
    [SerializeField] GameObject menu;

    private int _count = 1;
    private Transform _parentAfterDrag;
    
    private void Awake()
    {
        _image = GetComponent<Image>();
        _itemNameText = GameObject.Find("ItemName").GetComponent<TextMeshProUGUI>();
        _itemDescText = GameObject.Find("ItemDesc").GetComponent<TextMeshProUGUI>();
        
    }

    public void InitialiseItem(Item newItem)
    {
        _item = newItem;
        _image.sprite = newItem.ItemSprite;
        CountItem();
        
    }

    public void CountItem()
    {
        _countText.text = _count.ToString();
        bool textActive = _count > 1;
        _countText.gameObject.SetActive(textActive);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
       Debug.Log("Begin Drag");
       _parentAfterDrag = transform.parent;
       transform.SetParent(transform.root);
       transform.SetAsLastSibling();
       _image.raycastTarget = false;
       
    }
    
    

    public void OnDrag(PointerEventData eventData)
    {
        //Debug.Log("Dragging");
        Vector3 mousePosition = new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y);
        transform.position = mousePosition;
        _image.color = new Color(_image.color.r, _image.color.g, _image.color.b, 0.5f);
        
        
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        menu = GameObject.Find("DropPanel");
        RectTransform invPanel = menu.transform as RectTransform;
        Vector3 mousePosition = new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y);
        if (!RectTransformUtility.RectangleContainsScreenPoint(invPanel, mousePosition))
        {
            Debug.Log("Drop Item");
           Destroy(gameObject);
        }
        else
        {
            Debug.Log("End Drag");
            transform.SetParent(_parentAfterDrag);
            _image.raycastTarget = true;
            _image.color = new Color(_image.color.r, _image.color.g, _image.color.b, 1f);
        }
        
    }

    

    
    
    

    public Transform ParentAfterDrag
    {
        get { return _parentAfterDrag; }
        set { _parentAfterDrag = value; }
    }
    
    
    
    public int ItemCount
    {
        get { return _count; }
        set => _count = value;
    }

    public Item Item
    {
        get { return _item; }
    }


    public void OnPointerClick(PointerEventData eventData)
    {
        int clickCount = eventData.clickCount;

        if (clickCount == 1)
            OnSingleClick();
        else if (clickCount == 2)
            OnDoubleClick();
    }
    
    void OnSingleClick()
    {
        _itemNameText.text = _item.ItemName;
        _itemDescText.text = _item.ItemDescription;
    }

    void OnDoubleClick()
    {
        if (_item.ItemTypeCategory == Item.ItemType.Consumable)
        {
            Debug.Log($"{_item.ItemName} : {_item.ItemDescription}... consuming");
            Destroy(gameObject);
        }
        else if (_item.ItemTypeCategory == Item.ItemType.Equipment)
        {
            Debug.Log($"{_item.ItemName} : {_item.ItemDescription}");
        }
        
    }
}
