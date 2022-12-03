using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour , IDropHandler, IPointerEnterHandler, IPointerExitHandler
{
    public Image image;
    public Color selectedColor, notSelectedColor;
    public RectTransform invPanel;
    

    //public InventoryManager inventoryManager;

    private void Awake()
    {
        Deselect();
    }

    public void Deselect()
    {
        image.color = notSelectedColor;
    }
    
    public void OnDrop(PointerEventData eventData)
    {
        invPanel = transform as RectTransform;
        
        if (!RectTransformUtility.RectangleContainsScreenPoint(invPanel, Input.mousePosition))
        {
            Debug.Log("Drop Item");
        }
        
        if (transform.childCount == 0)
        {
            GameObject dropped = eventData.pointerDrag;
            InventoryItem item = dropped.GetComponent<InventoryItem>();
            item.ParentAfterDrag = transform;
        }
        
        
    }


    public void OnPointerEnter(PointerEventData eventData)
    {
        image.color = selectedColor;
        
    }


    public void OnPointerExit(PointerEventData eventData)
    {
        image.color = notSelectedColor;
    }



}