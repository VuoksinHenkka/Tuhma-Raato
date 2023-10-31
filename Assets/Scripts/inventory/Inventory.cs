using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Inventory : MonoBehaviour
{
    public Canvas ourInventoryCanvas;
    public List<InventoryCell> ourInventoryCells;
    public InventoryCell SelectedCell;
    public TMP_Text Selected_Description;

    private void Awake()
    {
        ourInventoryCanvas.enabled = false;
        foreach(InventoryCell foundcell in ourInventoryCells)
        {
            foundcell.ourInventory = this;
        }
    }

    private void Update()
    {
        if (GameManager.Instance.currentGameSate == GameManager.gamestate.Menu || GameManager.Instance.currentGameSate == GameManager.gamestate.GameOver) return;
        if (Input.GetKeyDown(KeyCode.Tab)) InventoryCall();


        if (GameManager.Instance.currentGameSate == GameManager.gamestate.Inventory)
        {
            if (Input.GetKeyDown(KeyCode.Escape)) InventoryCall();
        }
    }


    public void InventoryCall()
    {
        if (ourInventoryCanvas.enabled == false)
        {
            GameManager.Instance.currentGameSate = GameManager.gamestate.Inventory;
            RefreshInventory();
        }
        else
        {
            GameManager.Instance.currentGameSate = GameManager.gamestate.Gameplay;
        }

        ourInventoryCanvas.enabled = !ourInventoryCanvas.enabled;
    }

    private void RefreshInventory()
    {

    }

    public void ShowDescription(InventoryCell toDescribe)
    {
       Selected_Description.text = toDescribe.ourItem.Description;
    }

    public void SelectCell(InventoryCell toSelect)
    {
        SelectedCell = toSelect;
        RefreshInventory();
        InventoryCall();
    }
}
