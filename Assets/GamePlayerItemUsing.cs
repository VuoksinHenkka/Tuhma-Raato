using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlayerItemUsing : MonoBehaviour
{


    public GameObject currentTarget;
    public Dictionary<Item, Sprite> data_dictionaryOfItemSprites;
    public List<Item> data_StoredItems;
    public Item currentItem;
    public float cooldown = 0;
    public LineRenderer ourInteractionLine;
    public LayerMask forInteractionRayCast;
    public characterGFX ourCharacterGFX;

    private void Awake()
    {
        data_StoredItems = new List<Item>();
        //data_StoredItems.Add(new Item("Nothing", 1f, 4f));
        data_StoredItems.Add(new Item("Axe", 1.5f, 4f));
        currentItem = data_StoredItems[0];
    }

    void Update()
    {
        if (currentTarget && ourCharacterGFX) ourCharacterGFX.LookToDirection_override = currentTarget.transform.position;
        else if (!currentTarget && ourCharacterGFX) ourCharacterGFX.LookToDirection_override = Vector3.zero;
        if (cooldown != 0) cooldown = Mathf.Clamp(cooldown -= 1 * Time.deltaTime, 0, 100);

        if (Input.GetButtonDown("Fire1"))
        {
            Interact();
        }
        if (Input.GetButtonDown("Fire2"))
        {
            OpenInventory();
        }


        if (currentTarget == null) DrawInteractionLine(false);
        else DrawInteractionLine(true);
    }
    private void DrawInteractionLine(bool turnOn)
    {
        if (turnOn)
        {
            ourInteractionLine.SetPosition(0, ourInteractionLine.transform.position);
            ourInteractionLine.SetPosition(1, currentTarget.transform.position);
            if (ourInteractionLine.enabled == false) ourInteractionLine.enabled = true;
            if (IsInRange() == false) ourInteractionLine.SetColors(Color.red, Color.red);
            else ourInteractionLine.SetColors(Color.green, Color.green);
        }
        else ourInteractionLine.enabled = false;

    }

    private void FixedUpdate()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 100, forInteractionRayCast))
        {
            currentTarget = hit.collider.gameObject;
        }
        else currentTarget = null;
    }

    private void Interact()
    {
        if (cooldown != 0 || currentTarget == null || IsInRange() == false) return;
        else if (currentTarget.CompareTag("Item"))
        {
            currentTarget.BroadcastMessage("Receive", currentItem, SendMessageOptions.DontRequireReceiver);
            cooldown = currentItem.ActionCoolDown;
        }
    }

    private bool IsInRange()
    {
        return (Vector3.Distance(GameManager.Instance.ref_Player.transform.position, currentTarget.transform.position) < currentItem.SendRange);
    }

    private void OpenInventory()
    {

    }
}
