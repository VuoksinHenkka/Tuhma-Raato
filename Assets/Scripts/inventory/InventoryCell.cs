using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryCell : MonoBehaviour, ISelectHandler, IPointerEnterHandler, IPointerExitHandler
{

    public Inventory ourInventory;
    public ItemDefiner ourItem;
    private Image ourImage;
    public int ourIndex = 0;
    
    private void Awake()
    {
        ourImage = GetComponent<Image>();
    }
    public void OnSelect(BaseEventData eventData)
    {
        ourInventory.SelectCell(this);
    }

    public void SetItemTo(ItemDefiner newItem)
    {
        ourItem = newItem;
        ourImage.sprite = newItem.Icon;
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        ourInventory.ShowDescription(this);
    }
    public void OnPointerExit(PointerEventData eventData)
    {
    }
}