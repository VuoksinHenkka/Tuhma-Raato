using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    [Header("ITEM AND INVENTORY")]
    public Canvas ourInventoryCanvas;
    public Image itemInHand;
    public Image itemInHand_cooldown;

    public TMP_Text itemInHandName;
    public List<InventoryCell> ourInventoryCells;
    public InventoryCell SelectedCell;
    public TMP_Text Selected_Description;

    [Header("STATS")]
    public Canvas ourStatsCanvas;
    private float currentHP_magnitude = 1;
    private float currentStamina_magnitude = 1;
    public float currentHP = -1;
    public float currentStamine = -1;
    public TMP_Text HP_text;
    public TMP_Text Stamina_text;
    public Image HP_RadialFill;
    public Image Stamina_RadialFill;



    private void Awake()
    {
        ourInventoryCanvas.enabled = false;
        Selected_Description.text = "";
        foreach (InventoryCell foundcell in ourInventoryCells)
        {
            foundcell.ourInventory = this;
            foundcell.ourItem = GameManager.Instance.ref_ItemSolver.emptyItem;
        }
        for (int i =0; i < ourInventoryCells.Count-1; i++)
        {
            ourInventoryCells[i].ourIndex = i;
        }
    }

    private void Update()
    {
        itemInHand_cooldown.fillAmount = Mathf.InverseLerp(0, GameManager.Instance.MaxCooldown, GameManager.Instance.cooldownTimer);
        //STAT STUFF
        if (GameManager.Instance.currentGameSate == GameManager.gamestate.Menu && ourStatsCanvas.enabled) ourStatsCanvas.enabled = false;
        else if (ourStatsCanvas.enabled == false) ourStatsCanvas.enabled = true;

        currentHP_magnitude = Mathf.InverseLerp(0, 100, GameManager.Instance.ref_Stats.HP);
        currentStamina_magnitude = Mathf.InverseLerp(0, 100, GameManager.Instance.ref_Stats.Stamina);

        if (currentHP != currentHP_magnitude)
        {
            currentHP = currentHP_magnitude;
            HP_RadialFill.fillAmount = currentHP;
            HP_text.text = GameManager.Instance.ref_Stats.HP.ToString("000");
            if (currentHP > 0.75 && HP_RadialFill.color != Color.green) HP_RadialFill.color = Color.green;
            else if (currentHP > 0.25 && currentHP <= 0.75 && HP_RadialFill.color != Color.yellow) HP_RadialFill.color = Color.yellow;
            else if (currentHP <= 0.25 && HP_RadialFill.color != Color.red) HP_RadialFill.color = Color.red;
        }
        if (currentStamine != currentStamina_magnitude)
        {
            currentStamine = currentStamina_magnitude;
            Stamina_RadialFill.fillAmount = currentStamine;
            Stamina_text.text = GameManager.Instance.ref_Stats.Stamina.ToString("000");
            if (currentStamine > 0.75 && Stamina_RadialFill.color != Color.green) Stamina_RadialFill.color = Color.green;
            else if (currentStamine > 0.25 && currentStamine <= 0.75 && Stamina_RadialFill.color != Color.yellow) Stamina_RadialFill.color = Color.yellow;
            else if (currentStamine <= 0.25 && Stamina_RadialFill.color != Color.red) Stamina_RadialFill.color = Color.red;


        }

        //INVENTORY STUFF
        if (GameManager.Instance.currentGameSate == GameManager.gamestate.Menu || GameManager.Instance.currentGameSate == GameManager.gamestate.GameOver) return;
        if (Input.GetKeyDown(KeyCode.Tab)) InventoryCall();

        if (GameManager.Instance.currentGameSate == GameManager.gamestate.Inventory)
        {
            if (Input.GetKeyDown(KeyCode.Escape)) InventoryCall();
        }
        if (GameManager.Instance.ref_ItemSolver.currentlyHolding != null)
        {
            if (itemInHandName.text.Equals(GameManager.Instance.ref_ItemSolver.currentlyHolding.Name) == false)
            {
                itemInHandName.text = GameManager.Instance.ref_ItemSolver.currentlyHolding.Name;
                itemInHand.sprite = GameManager.Instance.ref_ItemSolver.currentlyHolding.Icon;
                itemInHand_cooldown.sprite = GameManager.Instance.ref_ItemSolver.currentlyHolding.Icon;

            }
        }


    }


    public void InventoryCall()
    {
        if (ourInventoryCanvas.enabled == false)
        {
            GameManager.Instance.currentGameSate = GameManager.gamestate.Inventory;
        }
        else
        {
            GameManager.Instance.currentGameSate = GameManager.gamestate.Gameplay;
        }

        ourInventoryCanvas.enabled = !ourInventoryCanvas.enabled;
        RefreshInventory();
    }

    private void RefreshInventory()
    {
        Selected_Description.text = "";
        for (int i = 0; i < 28; i++)
        {
            ourInventoryCells[i].SetItemTo(GameManager.Instance.ref_ItemSolver.PlayerItems[i]);
        }

            foreach(InventoryCell foundcell in ourInventoryCells)
            {
                foundcell.GetComponent<Selectable>().enabled = ourInventoryCanvas.enabled;
            }
    }

    public void ShowDescription(InventoryCell toDescribe)
    {
       Selected_Description.text = toDescribe.ourItem.Description;
    }

    public void SelectCell(InventoryCell toSelect)
    {
        if (toSelect.ourItem.itemType == ItemDefiner.Type.Consumable)
        {
            ConsumeItem(toSelect);
        }
        else
        {
            SelectedCell = toSelect;
            GameManager.Instance.ref_ItemSolver.currentlyHolding = SelectedCell.ourItem;
            GameManager.Instance.MaxCooldown = toSelect.ourItem.ActionCoolDown;
        }
    }
    public void ConsumeItem(InventoryCell toConsume)
    {
        ItemDefiner consumeparameters = toConsume.ourItem;
        GameManager.Instance.ref_Stats.HP_Modify(consumeparameters.GiveHealth);
        GameManager.Instance.ref_Stats.Stamina_Modify(consumeparameters.GiveStamina);
        int i = toConsume.ourIndex;
        GameManager.Instance.ref_ItemSolver.PlayerItems[i] = GameManager.Instance.ref_ItemSolver.emptyItem;
        RefreshInventory();
    }
}
