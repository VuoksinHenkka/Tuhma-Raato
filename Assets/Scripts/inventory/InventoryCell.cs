using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InventoryCell : MonoBehaviour, ISelectHandler
{

    public Inventory ourInventory;
    public Item ourItem;

    private void Awake()
    {
        ourItem = new Item("Nothing", 1, 5, 9999, "", 0, Item.Type.Generic);
    }

    public void OnSelect(BaseEventData eventData)
    {
        ourInventory.SelectCell(this);
    }
}