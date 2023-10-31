using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public Canvas ourInventoryCanvas;

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
            InventorySetup();
        }
        else GameManager.Instance.currentGameSate = GameManager.gamestate.Gameplay;

        ourInventoryCanvas.enabled = !ourInventoryCanvas.enabled;
    }

    private void InventorySetup()
    {

    }
}
